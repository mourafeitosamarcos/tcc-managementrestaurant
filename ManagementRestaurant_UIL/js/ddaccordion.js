//** Accordion Content script: By Dynamic Drive, at http://www.dynamicdrive.com
//** Created: Jan 7th, 08'. Last updated: Feb 16th, 2012 to v2.0

//Version 1.9: June 7th, 2010':
//**1) Ajax content support added, so a given header's content can be dynamically fetched from an external file and on demand.
//**2) Optimized script performance by caching header and content container references

//Version 2.0: Feb 16th, 2012':
//** 1) Added option ("scrolltoheader") to scroll to the expanded header in question after it expands (useful if a header contains long content).


var ddaccordion = {
    ajaxloadingmsg: '<img src="loading2.gif" /><br />Loading Content...', //customize HTML to output while Ajax content is being fetched (if applicable)

    headergroup: {}, //object to store corresponding header group based on headerclass value
    contentgroup: {}, //object to store corresponding content group based on headerclass value

    preloadimages: function($images) {
        $images.each(function() {
            var preloadimage = new Image();
            preloadimage.src = this.src;
        });
    },

    expandone: function(headerclass, selected, scrolltoheader) { //Função pública para expandir um determinado cabeçalho
        this.toggleone(headerclass, selected, "expand", scrolltoheader);
    },

    collapseone: function(headerclass, selected) { //Função pública em colapso um determinado cabeçalho
        this.toggleone(headerclass, selected, "collapse");
    },

    expandall: function(headerclass) { //Função pública para expandir todos os cabeçalhos com base em seu nome de classe CSS compartilhada
        var $headers = this.headergroup[headerclass];
        this.contentgroup[headerclass].filter(':hidden').each(function() {
            $headers.eq(parseInt($(this).attr('contentindex'))).trigger("evt_accordion");
        });
    },

    collapseall: function(headerclass) { //Função pública para recolher todos os cabeçalhos com base em seu nome de classe CSS compartilhada
        var $headers = this.headergroup[headerclass];
        this.contentgroup[headerclass].filter(':visible').each(function() {
            $headers.eq(parseInt($(this).attr('contentindex'))).trigger("evt_accordion");
        });
    },

    toggleone: function(headerclass, selected, optstate, scrolltoheader) { //Função pública para expandir / recolher um determinado cabeçalho
        var $targetHeader = this.headergroup[headerclass].eq(selected);
        var $subcontent = this.contentgroup[headerclass].eq(selected);
        if (typeof optstate == "undefined" || optstate == "expand" && $subcontent.is(":hidden") || optstate == "collapse" && $subcontent.is(":visible"))
            $targetHeader.trigger("evt_accordion", [false, scrolltoheader]);
    },

    ajaxloadcontent: function($targetHeader, $targetContent, config, callback) {
        var ajaxinfo = $targetHeader.data('ajaxinfo');

        function handlecontent(content) { //função aninhada
            if (content) { //se o conteúdo ajax carregou
                ajaxinfo.cacheddata = content; //lembre-se de conteúdo ajax
                ajaxinfo.status = "cached"; //definir o status de ajax para cache
                if ($targetContent.queue("fx").length == 0) { //se esse conteúdo não está expandindo ou recolhendo
                    $targetContent.hide().html(content); //esconder o carregamento de mensagens, em seguida, definir o conteúdo HTML sub para o conteúdo ajax
                    ajaxinfo.status = "complete"; //definir o status ajax para completar
                    callback(); //executar função de callback expandir este conteúdo sub
                }
            }
            if (ajaxinfo.status != "complete") {
                setTimeout(function() { handlecontent(ajaxinfo.cacheddata); }, 100); //call handlecontent() again until ajax content has loaded (ajaxinfo.cacheddata contains data)
            }
        }

        //acabar função aninhada

        if (ajaxinfo.status == "none") { //ajax dados não foi buscado ainda
            $targetContent.html(this.ajaxloadingmsg);
            $targetContent.slideDown(config.animatespeed);
            ajaxinfo.status = "loading"; //definir o status de ajax para "carregar"
            $.ajax({
                url: ajaxinfo.url, //caminho para o arquivo de menu externo
                error: function(ajaxrequest) {
                    handlecontent('Error fetching content. Server Response: ' + ajaxrequest.responseText);
                },
                success: function(content) {
                    content = (content == "") ? " " : content; //se o conteúdo retornado é vazio, configurá-lo para o "espaço" é o conteúdo não retorna mais falso / vazio (não foi carregado até o momento)
                    handlecontent(content);
                }
            });
        } else if (ajaxinfo.status == "loading")
            handlecontent(ajaxinfo.cacheddata);
    },

    expandit: function($targetHeader, $targetContent, config, useractivated, directclick, skipanimation, scrolltoheader) {
        var ajaxinfo = $targetHeader.data('ajaxinfo');
        if (ajaxinfo) { //se esse conteúdo deve ser buscado via Ajax
            if (ajaxinfo.status == "none" || ajaxinfo.status == "loading")
                this.ajaxloadcontent($targetHeader, $targetContent, config/*, function() { ddaccordion.expandit($targetHeader, $targetContent, config, useractivated, directclick); }*/);
            else if (ajaxinfo.status == "cached") {
                $targetContent.html(ajaxinfo.cacheddata);
                ajaxinfo.cacheddata = null;
                ajaxinfo.status = "complete";
            }
        }
        this.transformHeader($targetHeader, config, "expand");
        $targetContent.slideDown(skipanimation ? 0 : config.animatespeed, function() {
            config.onopenclose($targetHeader.get(0), parseInt($targetHeader.attr('headerindex')), $targetContent.css('display'), useractivated);
            if (scrolltoheader) {
                var sthdelay = (config["collapseprev"]) ? 20 : 0;
                clearTimeout(config.sthtimer);
                config.sthtimer = setTimeout(function() { ddaccordion.scrollToHeader($targetHeader); }, sthdelay);
            }
            if (config.postreveal == "gotourl" && directclick) { //se revealtype é "Ir para URL cabeçalho em clique", e este é um clique direto no cabeçalho
                var targetLink = ($targetHeader.is("a")) ? $targetHeader.get(0) : $targetHeader.find('a:eq(0)').get(0);
                if (targetLink) //se este cabeçalho é um link
                    setTimeout(function() { location = targetLink.href; }, 200 + (scrolltoheader ? 400 + sthdelay : 0)); //ignorar destino do link, como window.open (targetLink, targetLink.target) não funciona no FF se bloqueador de pop-habilitado
            }
        });
    },

    scrollToHeader: function($targetHeader) {
        ddaccordion.$docbody.stop().animate({ scrollTop: $targetHeader.offset().top }, 400);
    },

    collapseit: function($targetHeader, $targetContent, config, isuseractivated) {
        this.transformHeader($targetHeader, config, "collapse");
        $targetContent.slideUp(config.animatespeed, function() { config.onopenclose($targetHeader.get(0), parseInt($targetHeader.attr('headerindex')), $targetContent.css('display'), isuseractivated); });
    },

    transformHeader: function($targetHeader, config, state) {
        $targetHeader.addClass((state == "expand") ? config.cssclass.expand : config.cssclass.collapse) //alternativo btw "expandir" e "colapso" classes CSS
            .removeClass((state == "expand") ? config.cssclass.collapse : config.cssclass.expand);
        if (config.htmlsetting.location == 'src') { //Mudar imagem de cabeçalho (header assumindo é uma imagem)?
            $targetHeader = ($targetHeader.is("img")) ? $targetHeader : $targetHeader.find('img').eq(0); //Definir alvo para qualquer cabeçalho em si, ou primeira imagem dentro de cabeçalho
            $targetHeader.attr('src', (state == "expand") ? config.htmlsetting.expand : config.htmlsetting.collapse); //mudar a imagem do cabeçalho
        } else if (config.htmlsetting.location == "prefix") //se a mudança "prefixo" HTML, localize adicionados dinamicamente ". accordprefix" tag span e alterá-lo
            $targetHeader.find('.accordprefix').empty().append((state == "expand") ? config.htmlsetting.expand : config.htmlsetting.collapse);
        else if (config.htmlsetting.location == "suffix")
            $targetHeader.find('.accordsuffix').empty().append((state == "expand") ? config.htmlsetting.expand : config.htmlsetting.collapse);
    },

    urlparamselect: function(headerclass) {
        var result = window.location.search.match(new RegExp(headerclass + "=((\\d+)(,(\\d+))*)", "i")); //verificar "? headerclass = 2,3,4" na URL
        if (result != null)
            result = RegExp.$1.split(',');
        return result; //retorna nulo, [índice] ou [index1, index2, etc], onde o índice são os desejados índices de cabeçalho selecionados
    },

    getCookie: function(Name) {
        var re = new RegExp(Name + "=[^;]+", "i"); //construir RE para procurar par alvo de nome / valor
        if (document.cookie.match(re)) //se cookie encontrado
            return document.cookie.match(re)[0].split("=")[1]; //devolver seu valor
        return null;
    },

    setCookie: function(name, value) {
        document.cookie = name + "=" + value + "; path=/";
    },

    init: function(config) {
        document.write('<style type="text/css">\n');
        document.write('.' + config.contentclass + '{display: none}\n'); //gerar CSS para ocultar o conteúdo
        document.write('a.hiddenajaxlink{display: none}\n'); //Classe CSS para esconder ligação ajax
        document.write('<\/style>');
        jQuery(document).ready(function($) {
            ddaccordion.urlparamselect(config.headerclass);
            var persistedheaders = ddaccordion.getCookie(config.headerclass);
            ddaccordion.headergroup[config.headerclass] = $('.' + config.headerclass); //lembre-se de grupo de cabeçalho para este acordeão
            ddaccordion.contentgroup[config.headerclass] = $('.' + config.contentclass); //lembre-se de grupo de conteúdo para este acordeão
            ddaccordion.$docbody = (window.opera) ? (document.compatMode == "CSS1Compat" ? jQuery('html') : jQuery('body')) : jQuery('html,body');
            var $headers = ddaccordion.headergroup[config.headerclass];
            var $subcontents = ddaccordion.contentgroup[config.headerclass];
            config.cssclass = { collapse: config.toggleclass[0], expand: config.toggleclass[1] }; //armazenar expandir e contrair classes CSS como propriedades de objeto
            config.revealtype = config.revealtype || "click";
            config.revealtype = config.revealtype.replace(/mouseover/i, "mouseenter");
            if (config.revealtype == "clickgo") {
                config.postreveal = "gotourl"; //lembre-se de ação acrescentado
                config.revealtype = "click"; //substituir revealtype para "clicar" palavra-chave
            }
            if (typeof config.togglehtml == "undefined")
                config.htmlsetting = { location: "none" };
            else
                config.htmlsetting = { location: config.togglehtml[0], collapse: config.togglehtml[1], expand: config.togglehtml[2] }; //store HTML settings as object properties
            config.oninit = (typeof config.oninit == "undefined") ? function() {
            } : config.oninit; //attach custom "oninit" event handler
            config.onopenclose = (typeof config.onopenclose == "undefined") ? function() {
            } : config.onopenclose; //attach custom "onopenclose" event handler
            var lastexpanded = {}; //object to hold reference to last expanded header and content (jquery objects)
            var expandedindices = ddaccordion.urlparamselect(config.headerclass) || ((config.persiststate && persistedheaders != null) ? persistedheaders : config.defaultexpanded);
            if (typeof expandedindices == 'string') //test for string value (exception is config.defaultexpanded, which is an array)
                expandedindices = expandedindices.replace(/c/ig, '').split(','); //transform string value to an array (ie: "c1,c2,c3" becomes [1,2,3]
            if (expandedindices.length == 1 && expandedindices[0] == "-1") //check for expandedindices value of [-1], indicating persistence is on and no content expanded
                expandedindices = [];
            if (config["collapseprev"] && expandedindices.length > 1) //only allow one content open?
                expandedindices = [expandedindices.pop()]; //return last array element as an array (for sake of jQuery.inArray())
            if (config["onemustopen"] && expandedindices.length == 0) //if at least one content should be open at all times and none are, open 1st header
                expandedindices = [0];
            $headers.each(function(index) { //loop through all headers
                var $header = $(this);
                if (/(prefix)|(suffix)/i.test(config.htmlsetting.location) && $header.html() != "") { //add a SPAN element to header depending on user setting and if header is a container tag
                    $('<span class="accordprefix"></span>').prependTo(this);
                    $('<span class="accordsuffix"></span>').appendTo(this);
                }
                $header.attr('headerindex', index + 'h'); //store position of this header relative to its peers
                $subcontents.eq(index).attr('contentindex', index + 'c'); //store position of this content relative to its peers
                var $subcontent = $subcontents.eq(index);
                var $hiddenajaxlink = $subcontent.find('a.hiddenajaxlink:eq(0)'); //see if this content should be loaded via ajax
                if ($hiddenajaxlink.length == 1) {
                    $header.data('ajaxinfo', { url: $hiddenajaxlink.attr('href'), cacheddata: null, status: 'none' }); //store info about this ajax content inside header
                }
                var needle = (typeof expandedindices[0] == "number") ? index : index + ''; //check for data type within expandedindices array- index should match that type
                if (jQuery.inArray(needle, expandedindices) != -1) { //verificar cabeçalhos que deve ser expandido automaticamente (converter índice para primeira corda)
                    //Aqui e onde e expandido o menu automaticamente
                    //ddaccordion.expandit($header, $subcontent, config, false, false, !config.animatedefault); //Param última 3 define parâmetro 'isuseractivated', segundo isdirectclick conjuntos último, último conjuntos param skipanimation
                    lastexpanded = { $header: $header, $content: $subcontent };
                }  //end check
                else {
                    $subcontent.hide();
                    config.onopenclose($header.get(0), parseInt($header.attr('headerindex')), $subcontent.css('display'), false); //Valor booleano Última define parâmetro 'isuseractivated'
                    ddaccordion.transformHeader($header, config, "collapse");
                }
            }); //if (config["scrolltoheader"] && lastexpanded.$header)
            //ddaccordion.scrollToHeader(lastexpanded.$header)
            $headers.bind("evt_accordion", function(e, isdirectclick, scrolltoheader) { //atribuir manipulador de eventos personalizado que se expande / contatos um cabeçalho
                var $subcontent = $subcontents.eq(parseInt($(this).attr('headerindex'))); //obter subcontent que deve ser expandida / ruiu
                if ($subcontent.css('display') == "none") {
                    ddaccordion.expandit($(this), $subcontent, config, true, isdirectclick, false, scrolltoheader); //Param última segunda define parâmetro 'isuseractivated'
                    if (config["collapseprev"] && lastexpanded.$header && $(this).get(0) != lastexpanded.$header.get(0)) { //colapso conteúdo anterior?
                        ddaccordion.collapseit(lastexpanded.$header, lastexpanded.$content, config, true); //Valor booleano Última define parâmetro 'isuseractivated'
                    }
                    lastexpanded = { $header: $(this), $content: $subcontent };
                } else if (!config["onemustopen"] || config["onemustopen"] && lastexpanded.$header && $(this).get(0) != lastexpanded.$header.get(0)) {
                    ddaccordion.collapseit($(this), $subcontent, config, true); //Valor booleano Última define parâmetro 'isuseractivated'
                }
            });
            $headers.bind(config.revealtype, function() {
                if (config.revealtype == "mouseenter") {
                    clearTimeout(config.revealdelay);
                    var headerindex = parseInt($(this).attr("headerindex"));
                    config.revealdelay = setTimeout(function() { ddaccordion.expandone(config["headerclass"], headerindex, config.scrolltoheader); }, config.mouseoverdelay || 0);
                } else {
                    $(this).trigger("evt_accordion", [true, config.scrolltoheader]); //último parâmetro indica que este é um clique direto no cabeçalho
                    return false; //cancelar o comportamento padrão clique
                }
            });
            $headers.bind("mouseleave", function() {
                clearTimeout(config.revealdelay);
            });
            config.oninit($headers.get(), expandedindices);
            $(window).bind('unload', function() { //limpar e persistir na página descarregar
                $headers.unbind();
                var expandedindices = [];
                $subcontents.filter(':visible').each(function(index) { //os índices de cabeçalhos expandidas
                    expandedindices.push($(this).attr('contentindex'));
                });
                if (config.persiststate == true && $headers.length > 0) { //persistir o estado?
                    expandedindices = (expandedindices.length == 0) ? '-1c' : expandedindices; //Nenhum conteúdo expandido, indicam que com manequim valor "-1c '?
                    ddaccordion.setCookie(config.headerclass, expandedindices);
                }
            });
        });
    }
};
//pré-carregar as imagens definidas dentro ajaxloadingmsg variável
ddaccordion.preloadimages(jQuery(ddaccordion.ajaxloadingmsg).filter('img'));