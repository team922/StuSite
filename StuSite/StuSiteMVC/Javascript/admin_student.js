function getcollege() {
    $.ajax({
        url: '/Admin/GetCollege',
        success: function (data) {
            var college = $.parseJSON(data);
            for (var i = 0; i < college.number ; i++) {
                addcollege(college.collegelist[i].id, college.collegelist[i].name);
            }
            document.getElementById("college_loading").text = "请选择院系！";
        }
    });
}

function getmajor() {
    var div = document.getElementById("select_major");
    while (div.hasChildNodes()) {
        div.removeChild(div.firstChild);
    }
    $("#select_major").append("<option value='' id='major_loading'>请先选择院系！</option>");
    var collegeid = document.getElementById("select_college").value;
    $.ajax({
        url: '/Admin/GetSelectMajor',
        data: { "collegeid": collegeid },
        success: function (data) {
            var major = $.parseJSON(data);
            for (var i = 0; i < major.number ; i++) {
                addmajor(major.majorlist[i].id, major.majorlist[i].name);
            }
            document.getElementById("major_loading").text = "请选择专业！";
        }
    });
}

function addcollege(id, name) {
    var college = "<option value='" + id + "'>" + name + "</option>";
    $("#select_college").append(college);
}

function addmajor(id, name) {
    var major = "<option value='" + id + "' id='majorlist'>" + name + "</option>";
    $("#select_major").append(major);
}