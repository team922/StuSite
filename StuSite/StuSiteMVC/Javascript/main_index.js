function getipmessage() {
    $.ajax({
        url: '/Home/GetIPMessage',
        success: function (data) {
            //alert(data);
            var ipmessage = $.parseJSON(data);
            //alert(ipmessage.errMsg);
            if (ipmessage.errMsg == "success") {
                //alert(ipmessage.retData.ip);
                //alert(ipmessage.retData.country);
                //alert(ipmessage.retData.province);
                //alert(ipmessage.retData.district);
                //alert(ipmessage.retData.carrier);
                addipmessage(ipmessage.retData.ip, ipmessage.retData.country, ipmessage.retData.province, ipmessage.retData.city, ipmessage.retData.district, ipmessage.retData.carrier);
                getareaid(ipmessage.retData.province, ipmessage.retData.city, ipmessage.retData.district);
            }
            else {
                document.getElementById("weather_loading").style.display = "none";
                $("#showipmessage").append("IPMessage加载失败");
            }
        }
    });
}

function getareaid(province, city, district) {
    //alert("getareaid_start");
    var area_id;
    $.ajax({
        url: '/Home/GetAreaid/',
        data: { district: encodeURI(district) },
        success: function (data) {
            //alert(data);
            var area = $.parseJSON(data);
            //alert(area.errMsg);
            if (area.errMsg == "success") {
                //alert(area.retData.length);
                for (var i = 0; i < area.retData.length; i++) {
                    if (area.retData[i].province_cn == province && area.retData[i].district_cn == city && area.retData[i].name_cn == district) {
                        area_id = area.retData[i].area_id;
                    }
                    else {
                        area_id = "000000000";
                    }
                }
                if (area_id == "000000000") {
                    document.getElementById("weather_loading").style.display = "none";
                    $("#showweather").append("<h2 style='float:left'>暂无该地区信息！</h2>");
                }
                else {
                    getweather(area_id);
                }
            }
            else {
                document.getElementById("weather_loading").style.display = "none";
                $("#weather_display").append("<h2 style='float:left'>" + area.errMsg + "</h2>");
            }
        }
    });
}

function getweather(id) {
    //alert("getweather_start");
    $.ajax({
        url: '/Home/GetWeather/',
        data: { id: id },
        success: function (data) {
            //alert(data);
            var weather = $.parseJSON(data);
            //alert(weather.errMsg);
            if (weather.errMsg == "success") {
                //alert(weather.retData);
                addweather(weather.retData.weather, weather.retData.l_tmp, weather.retData.h_tmp, weather.retData.WD, weather.retData.WS, weather.retData.date, weather.retData.time);
                document.getElementById("weather_loading").style.display = "none";
            }
            else {
                document.getElementById("weather_loading").style.display = "none";
                $("#showweather").append("<h2 style='float:left'>" + weather.errMsg + "</h2>");
            }
        }
    });
}

function getdepartment() {
    //alert("getdepartment_start");
    $.ajax({
        url: '/Home/GetDepartment',
        success: function (data) {
            //alert(data);
            var department = $.parseJSON(data);
            for (var i = 0; i < department.number ; i++) {
                adddepartment(department.departmentlist[i].id, department.departmentlist[i].name);
            }
        }
    });
}

function gettopnotice() {
    $.ajax({
        url: '/Home/GetTopNotice',
        success: function (data) {
            //alert("gettopnotice" + data);
            if (data == "false") {
                //alert("false");
                document.getElementById("top_notice").style.display = "";
            }
            else {
                //alert("true");
                var notice = $.parseJSON(data);
                //alert("top");
                addtopnotice(notice.id, notice.title, notice.belong, notice.date);
            }
        }
    });
}

function getnormalnotice() {
    for (var i = 0; i < 10; i++) {
        $(".normal_notice").remove();
    }
    document.getElementById("notice_loading").style.display = "";
    var department = document.getElementById("select1").value;
    //alert(department);
    $.ajax({
        url: '/Home/GetSelectNotice/',
        data: { department: department },
        success: function (data) {
            //alert("getnormalnotice" + data);
            var notice = $.parseJSON(data);
            //alert(notice.number);
            if (notice.number == 0) {
                $("#normal_notice_list").append("<div class='normal_notice'>暂无新闻</div>");
                $("#notice_bottom").remove();
            }
            for (var i = 0; i < notice.number && i < 10 ; i++) {
                addnormalnotice(notice.listnotice[i].id, notice.listnotice[i].title, notice.listnotice[i].belong, notice.listnotice[i].date);
            }
            document.getElementById("notice_loading").style.display = "none";
        }
    });
}

function gettopnews() {
    //alert("gettopnews_start");
    $.ajax({
        url: '/Home/GetTopNews',
        success: function (data) {
            //alert("gettopnews" + data);
            if (data == "false") {
                document.getElementById("top_news").style.display = "";
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
                addtopnews(news.id, news.title, news.date);
            }
        }
    });
}

function getnormalnews() {
    //alert("getnews_start");
    document.getElementById("news_loading").style.display = "";
    $.ajax({
        url: '/Home/GetNews',
        success: function (data) {
            //alert("getnormalnews" + data);
            var news = $.parseJSON(data);
            //alert(news.number);
            if (news.number == 0) {
                $("#normal_news_list").append("<div class='normal_news'>暂无新闻</div>");
            }
            for (var i = 0; i < news.number && i < 10 ; i++) {
                //alert(news.listnews[i].title);
                //alert(news.listnews[i].date);
                addnormalnews(news.listnews[i].id, news.listnews[i].title, news.listnews[i].date);
            }
            document.getElementById("news_loading").style.display = "none";
        }
    });
}

function addipmessage(ip, country, province, city, district, carrier) {
    //alert("addipmessage");
    //var message = "ＩＰ地址：" + ip + "<br />国　　家：" + country + "<br />省　　份：" + province + "<br />城　　市：" + city + "<br />地　　区：" + district + "<br />运 营 商：" + carrier + "<br />";
    var message = "";
    message += "<b>ＩＰ地址：" + ip + "</b><br /><b>国　　家：" + country + "</b><br />";
    message += "<b>省　　份：" + province + "</b><br /><b>城　　市：" + city + "</b><br />";
    message += "<b>地　　区：" + district + "</b><br /><b>运 营 商：" + carrier + "</b><br />";
    //alert(message);
    $("#showipmessage").append(message);
}

function addweather(weather, l_tmp, h_tmp, WD, WS, date, time) {
    weather = "<b>天气情况：" + weather + "</b><br /><b>最低气温：" + l_tmp + "</b><br /><b>最高气温：" + h_tmp + "</b><br /><b>风　　向：" + WD + "</b><br /><b>风　　力：" + WS + "</b><br /><b>发布时间：" + date + "　" + time + "</b><br />";
    $("#showweather").append(weather);
}

function adddepartment(id, name) {
    var department = "<option value='" + id + "'>" + name + "</option>";
    $("#select1").append(department);
}

function addtopnotice(id, title, belong, date) {
    var topnotice = "<img class='a_img' src='/Images/main_istop.png' /><a id='" + id + "' class='notice_title' href='#' onclick='opendetail(this);'>" + title + "</a><div class='notice_date'>" + date + "</div><div class='notice_belong'>" + belong + "</div>";
    $("#top_notice").append(topnotice);
}

function addnormalnotice(id, title, belong, date) {
    var notice = "<div class='normal_notice'><img class='a_img' src='/Images/main_right.png' /><a id='" + id + "' class='notice_title' href='#' onclick='opendetail(this);'>" + title + "</a><div class='notice_date'>" + date + "</div><div class='notice_belong'>" + belong + "</div></div>";
    $("#normal_notice_list").append(notice);
}

function addtopnews(id, title, date) {
    var topnews = "<img class='a_img' src='/Images/main_istop.png' /><a id='" + id + "' class='news_title' href='#' onclick='opendetail(this);'>" + title + "</a><span class='news_time'>" + date + "</span>";
    $("#top_news").append(topnews);
}

function addnormalnews(id, title, date) {
    var news = "<div class='normal_news'><img class='a_img' src='/Images/main_right.png' /><a id='" + id + "' class='news_title' href='#' onclick='opendetail(this);'>" + title + "</a><span class='news_time'>" + date + "</span></div>";
    $("#normal_news_list").append(news);
}