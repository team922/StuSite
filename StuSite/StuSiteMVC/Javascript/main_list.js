function gettopicnotice() {
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

function GetNoticeList() {
    var url = "/Home/ShowAllList" + location.search;
    $.ajax({
        url: url,
        success: function (data) {
            var list = $.parseJSON(data);
            for (var i = 0; i < list.number ; i++) {
                addnormalnotice(list.listnotice[i].id, list.listnotice[i].title, list.listnotice[i].belong, list.listnotice[i].date);
            }
        }
    });
}

function GetNewsList() {
    var url = "/Home/ShowAllList" + location.search;
    $.ajax({
        url: url,
        success: function (data) {
            var list = $.parseJSON(data);
            for (var i = 0; i < list.number ; i++) {
                addnormalnews(list.listnews[i].id, list.listnews[i].title, list.listnews[i].date);
            }
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