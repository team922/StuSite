function gettopicnotice() {
    var div = document.getElementById("list_topic");
    while (div.hasChildNodes()) {
        div.removeChild(div.firstChild);
    }
    $.ajax({
        url: '/Home/GetTopNotice',
        success: function (data) {
            //alert("gettopnotice" + data);
            if (data == "false") {
                //alert("false");
            }
            else {
                //alert("true");
                var notice = $.parseJSON(data);
                //alert("top");
                addtopicnotice(notice.id, notice.title, notice.belong, notice.date);
            }
        }
    });
}

function gettopicnews() {
    var div = document.getElementById("list_topic");
    while (div.hasChildNodes()) {
        div.removeChild(div.firstChild);
    }
    //alert("gettopnews_start");
    $.ajax({
        url: '/Home/GetTopNews',
        success: function (data) {
            //alert("gettopnews" + data);
            if (data == "false") {
            }
            else {
                //alert("true1");
                //alert("true2");
                var news = $.parseJSON(data);
                //alert("true3");
                //alert("id" + news.id);
                //alert("title" + news.title);
                //alert("date" + news.date);
                //alert("publisher" + news.publisher);
                //alert("hits" + news.hits);
                addtopicnews(news.id, news.title, news.date);
            }
        }
    });
}

function GetNoticeList(pagesize, pageindex) {
    var div = document.getElementById("list_normal_all");
    while (div.hasChildNodes()) {
        div.removeChild(div.firstChild);
    }
    var url = "/Home/ShowAllList" + location.search + "&pagesize=" + pagesize + "&pageindex=" + pageindex;
    $.ajax({
        url: url,
        success: function (data) {
            var list = $.parseJSON(data);
            for (var i = 0; i < list.number ; i++) {
                addnormalnotice(list.listnotice[i].id, list.listnotice[i].title, list.listnotice[i].belong, list.listnotice[i].date);
            }
            make_page_list(list.pagecount, list.pageindex);
            document.getElementById("list_loading").style.display = "none";
        }
    });
}

function GetNewsList(pagesize, pageindex) {
    var div = document.getElementById("list_normal_all");
    while (div.hasChildNodes()) {
        div.removeChild(div.firstChild);
    }
    var url = "/Home/ShowAllList" + location.search + "&pagesize=" + pagesize + "&pageindex=" + pageindex;
    $.ajax({
        url: url,
        success: function (data) {
            var list = $.parseJSON(data);
            for (var i = 0; i < list.number ; i++) {
                addnormalnews(list.listnews[i].id, list.listnews[i].title, list.listnews[i].date);
            }
            make_page_list(list.pagecount, list.pageindex);
            document.getElementById("list_loading").style.display = "none";
        }
    });
}

function addtopicnotice(id, title, belong, date) {
    var topic = "<img src='/Images/main_istop.png' /><a id='" + id + "' class='notice_title' href='###' onclick='opendetail(this);'>" + title + "</a><div class='list_date'>" + date + "</div><div class='list_belong'>" + belong + "</div>";
    //alert(topic);
    $("#list_topic").append(topic);
}

function addnormalnotice(id, title, belong, date) {
    var list = "<div class='list_normal'><img src='/Images/main_right.png' /><a id='" + id + "' class='notice_title' href='###' onclick='opendetail(this);'>" + title + "</a><div class='list_date'>" + date + "</div><div class='list_belong'>" + belong + "</div></div>";
    //alert(list);
    $("#list_normal_all").append(list);
}

function addtopicnews(id, title, date) {
    var topic = "<img src='/Images/main_istop.png' /><a id='" + id + "' class='news_title' href='###' onclick='opendetail(this);'>" + title + "</a><div class='list_date'>" + date + "</div>";
    $("#list_topic").append(topic);
}

function addnormalnews(id, title, date) {
    var list = "<div class='list_normal'><img src='/Images/main_right.png' /><a id='" + id + "' class='news_title' href='###' onclick='opendetail(this);'>" + title + "</a><div class='list_date'>" + date + "</div></div>";
    $("#list_normal_all").append(list);
}

function make_page_list(pagecount, pageindex) {

    $("#page_list_table").remove();

    var firstpage = 'GetList(' + 1 + ')';
    var lastpage = 'GetList(' + pagecount + ')';
    var previouspage = pageindex - 1;
    var nextpage = +pageindex + 1;

    if (pagecount > 1) {
        var html = '<table id="page_list_table" cellpadding="0" width="700px" cellspacing="0" height="40" align="center"> <tr>';

        if (pageindex > 1) {
            html += '<td align="left"><a href="javascript:' + firstpage + ';">首页</a>&nbsp;&nbsp;';
        } else {
            html += '<td align="left">首页&nbsp;&nbsp;';
        }

        if (pageindex > 1) {
            html += '|&nbsp;<a href="javascript:GetList(' + previouspage + ');">上一页</a>&nbsp;';
        } else {
            html += '|&nbsp;上一页&nbsp;';
        }

        if (pageindex < pagecount) {
            html += '|&nbsp;<a href="javascript:GetList(' + nextpage + ');">下一页</a>&nbsp;';
        } else {
            html += '|&nbsp;下一页&nbsp;';
        }

        if (pageindex < pagecount) {
            html += '|&nbsp;<a href="javascript:' + lastpage + ';">末页</a></td>';
        }
        else {
            html += '|&nbsp;末页</td>';
        }

        html += '<td align="right" >第' + pageindex + '页/共' + pagecount + '页&nbsp;&nbsp;';
        for (var i = 1; i <= pagecount; i++) {
            if (i == pageindex) {
                html += '&nbsp;[' + i + ']&nbsp;';
            } else {
                html += '&nbsp;<a href="javascript:GetList(' + i + ');">[' + i + ']</a>';
            }
        }
        html += '</td></tr></table>';
    }
    //alert(html);
    //document.write(html);
    $("#page_list").append(html);
}