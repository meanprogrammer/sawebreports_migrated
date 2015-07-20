/*================================================================*/
/* GENERAL */

//function createXMLHttpRequest() {
//    var message = '';
//    try {
//        return new XMLHttpRequest();
//    }
//    catch (e) {
//        message += 'Cannot create XMLHttpRequest();';
//    }

//    try {
//        return new ActiveXObject("Msxml2.XMLHTTP");
//    }
//    catch (e) {
//        message += 'Cannot create ActiveXObject("Msxml2.XMLHTTP");';
//    }

//    try {
//        return new ActiveXObject("Microsoft.XMLHTTP");
//    }
//    catch (e) {
//        message += 'Cannot create ActiveXObject("Microsoft.XMLHTTP");';
//    }
//    alert(message);
//    return null;
//}

//function RenderEntity(url, id) {
//    showLoader(true);    
//    var xhr = createXMLHttpRequest();
//    var ts = new Date().getTime().toString();
//    url = url + '&ts=' + ts;
//    xhr.onreadystatechange = function() {
//        if (xhr.readyState == 4 && xhr.status == 200) {
//            BuildContent(xhr, id);
//            document.getElementById('paramTracker').value = url + '|' + id;
//        }
//    };
//    xhr.open("GET", url, true);
//    xhr.send();
//}

//function BuildContent(xhr, id) {
//    var results, _window;
//    if (xhr.responseText != 'undefined' && xhr.responseText != '') {
//        
//        if (xhr.responseText.indexOf("ErrorPage") >= 0) {
//            document.write(xhr.responseText);
//            showLoader(false);
//            return;
//        }
//    
//        if (xhr.responseText.indexOf("{split}") >= 0) {
//            results = xhr.responseText.split("{split}");
//            getElement('header-caption').innerHTML = results[0];
//            getElement('content').innerHTML = results[1];
//            getElement('content').scrollTop = 0;
//        } else {
//            _window = window.open("", "_blank", "width=800,height=400,resizable=1,scrollbars=1");
//            _window.document.write("<link rel=\"Stylesheet\" type=\"text/css\" href=\"styles/styles.css\" />");
//            _window.document.write(xhr.responseText);
//        }
//        setSelectedMenuItem(id);
//        showLoader(false);
//    }
//}


//function toggleNavigation() {
//    alert('dsadasdas');
//    var button, navigation, content, container;
//    button = $('#showhide');

//    navigation = $('#navigation');
//    content = $('#content');
//    container = $('#hide-container');
//    if (button.attr('title') == 'hide') {
//        $('#navigation-small').show();
//        $('#navigation').hide();
//        content.css('width', '97%');
//        content.css('left','24px');
//        content.css('margin-left','0');
//        container.css('left','0');
//        button.attr('class', 'show-button');
//        button.attr('title','show');
//        container.hide();
//    } else {
//        $('#navigation-small').hide();
//        $('#navigation').show();
//        content.css('width','79%');
//        content.css('left','20%');
//        content.css('margin-left','0');
//        container.css('left','18%');
//        button.attr('class','hide-button');
//        button.attr('title','hide');
//        container.show();
//    }
//}

//function showLoader(show) {
//    var display = show ? 'block' : 'none';
//    $('#loader-background').style.display = display;
//    getElement('loader').style.display = display;
//}

function generateReport() {
    var tracker, _window;
    tracker = $('#reportId').val();
    if (tracker == '' || tracker == undefined) {
        alert('Select a diagram first.');
        return;
    }

    _window = window.open("GenerateReport.aspx?reportid=" + tracker, "_blank");
}

/* GENERAL */
/*================================================================*/

function showAsIsDiagramItemDetails(id) {
    var position, element, xPos, yPos;
    element = $('#'+id);
    if (element)
    {

        element.css('display','block');
        position = GetTopLeft(id + '_main');
        xPos = position.x - 20;
        yPos = position.y + 60;
        element.css('top', yPos + 'px');
        element.css('left',xPos + 'px');
        element.css('z-index','1000');
    }
}

//function showQuickLinksMenu(id) {
//    var position, element, xPos, yPos;
//    element = getElement(id);
//    if (element) {
//        element.style.display = 'block';

//        position = GetTopLeft(id + '_div');
//        xPos = position.x - 100;
//        yPos = position.y + 18;

//        element.style.top = yPos + 'px';
//        element.style.left = xPos + 'px';
//        element.style.zIndex = 1000;
//    }
//}

function getElement(id) {
    return document.getElementById(id);
}

function hideElement(id) {
    if (getElement(id)) {
        getElement(id).style.display = 'none';
    }
}

function showElement(id) {
    if (getElement(id)) {
        getElement(id).style.display = 'block';
    }
}

function GetOffset(object, offset) {
    if (!object)
        return;
    offset.x += object.offsetLeft;
    offset.y += object.offsetTop;

    GetOffset(object.offsetParent, offset);
}

function GetScrolled(object, scrolled) {
    if (!object)
        return;
    scrolled.x += object.scrollLeft;
    scrolled.y += object.scrollTop;

    if (object.tagName.toLowerCase() != "html") {
        GetScrolled(object.parentNode, scrolled);
    }
}

function GetTopLeft(id) {
    var div, offset, scrolled, posX, posY;
    div = getElement(id);

    offset = { x: 0, y: 0 };
    GetOffset(div, offset);

    scrolled = { x: 0, y: 0 };
    GetScrolled(div.parentNode, scrolled);

    posX = offset.x - scrolled.x;
    posY = offset.y - scrolled.y;

    return { x: posX, y: posY };
}


//function ShowMenu(e) {
//    var ulElems = e.getElementsByTagName('ul');
//    ulElems[0].style.display = 'block';
//    ulElems[0].style.visibility = 'visible';
//}

//function HideMenu(e) {
//    var ulElems = e.getElementsByTagName('ul');
//    ulElems[0].style.display = 'none';
//    ulElems[0].style.visibility = 'hidden';
//}


function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}

function setcontent(id) {
    $("#dialog-modal").html(id);
}

//function getacronymdetails(id, title) {
//    var url = 'Detail.aspx?detailID=' + id;
//    $.ajax({
//        url: url,
//        type: "post",
//        dataType: "html",
//        cache: true,
//        error: function() {
//            alert('detail ajax call error.');
//        },
//        success: function(strData) {
//            
//        }
//    });



//    // Prevent default click.
//    return false;	 
//}

//function getDetailAjax(id, title, isctl) {
//    var url;
//    var width = 700;
//    if (isctl) {
//        url = 'Detail.aspx?detailID=' + id + '&ctl=true';
//        width = 900;
//    } else {
//        url = 'Detail.aspx?detailID=' + id;
//    }

//    $.ajax({
//        url: url,
//        type: "post",
//        dataType: "html",
//        cache: true,
//        error: function() {
//            alert('detail ajax call error.');
//        },
//        success: function(strData) {
//            if ($("#modal-popup").length == 0) {
//                $("<div/>", { id: 'modal-popup' }
//                ).appendTo($("#content"));

//                $("#modal-popup").dialog({
//                    modal: true,
//                    autoOpen: false,
//                    minWidth: 500,
//                    maxWidth: 1000,
//                    width: width,
//                    draggable: false,
//                    resizable: false,
//                    close: function(event, ui) {
//                        $("#modal-popup").html('');
//                        $("#modal-popup").tooltip('destroy');
//                    },
//                    buttons: {
//                        Close: function() { $(this).dialog("close"); }
//                    }
//                });

//            }

//            $("#modal-popup").dialog("option", "title", title);
//            $("#modal-popup").html(strData);
//            $("#modal-popup").dialog("open");
//            $("#modal-popup").tooltip({
//                items: "[tag]",
//                tooltipClass: 'tooltip',
//                content: function() {
//                    var element, result, detailId;
//                    element = $(this);
//                    if (element.is("[tag]")) {
//                        //console.log(element.attr('tag'));
//                        //return "<table><tr><td>Description:</td><td></td></tr><tr><td>Reference Documents:</td><td></td></tr></table>";
//                        detailId = element.attr('tag');
//                        if (detailId == '' || detailId == undefined) {
//                            return "<table><tr><td>Description:</td><td></td></tr><tr><td>Reference Documents:</td><td></td></tr></table>";
//                        }


//                        result = "";
//                        result = document.getElementById('div_' + detailId).innerHTML;

//                        //                $.ajax({
//                        //                    url: 'Detail.aspx?detailID=' + detailId,
//                        //                    type: "post",
//                        //                    dataType: "html",
//                        //                    cache: true,
//                        //                    error: function() {
//                        //                        alert('detail ajax call error.');
//                        //                    },
//                        //                    success: function(strData) {
//                        //                        result = strData;
//                        //                    }
//                        //                });
//                        return result;
//                    }
//                }
//            });





//        }
//    });



//    // Prevent default click.
//    return false;
//}

function showloader() {
    $('#ajax-loader').modal({ keyboard: false });
}

function hideloader() {
    $('#ajax-loader').modal('hide');
}

function getDetailAjax(id, title, isctl) {
    var url;
    var width = 700;
    if (isctl) {
        url = 'Detail.aspx?detailID=' + id + '&ctl=true';
        width = 900;
    } else {
        url = 'Detail.aspx?detailID=' + id;
    }

    $.ajax({
        url: url,
        type: "post",
        dataType: "html",
        cache: true,
        error: function() {
            alert('detail ajax call error.');
        },
        success: function(data) {
            var obj = JSON.parse(data);
            if (obj != null) {
                var source = $(obj.TemplateID).html();
                var template = Handlebars.compile(source);
                var content = template(obj);
                $('#detail-modal').html(content);
                isctl ? $('.modal-dialog').addClass('modal-lg') : $('.modal-dialog').addClass('modal-md');
                $('#detail-modal').modal({ keyboard: false });
            }
        }
    });

    // Prevent default click.
    return false;
}



jQuery.fn.highlight = function(str, className) {
    var regex = new RegExp(str, "gi");
    return this.each(function() {
        $(this).contents().filter(function() {
            return this.nodeType == 3 && regex.test(this.nodeValue);
        }).replaceWith(function() {
            return (this.nodeValue || "").replace(regex, function(match) {
                return "<span class=\"" + className + "\">" + match + "</span>";
            });
        });
    });
};


function init_quicklinks() {

    $("ul.dropdown li").hover(function() {

        $(this).addClass("hover");
        $('ul:first', this).css('visibility', 'visible');

    }, function() {

        $(this).removeClass("hover");
        $('ul:first', this).css('visibility', 'hidden');

    });

    $("ul.dropdown li ul li:has(ul)").find("a:first").append(" &raquo; ");

}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
} 