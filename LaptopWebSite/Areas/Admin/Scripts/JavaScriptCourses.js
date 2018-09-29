SetCourses();
function SetCourses() {
    $("#btnContact").removeClass("active");
    $("#btnStudent").removeClass("active");
    $("#btnCourse").addClass("active");
}

$("#btnAddNewCourses").on("click", AddCourses);

function AddCourses() {

    var name = $("#txtNameCourses").val();
    var date = $("#txtDate").val();
    var description = $("#txtDescriptionCourses").val();

    if (name != "" && description != "" && date != "") {

        var courses = {
            Name: name,
            StudyDate: date,
            Description: description
        }

        $.ajax({
            url: 'https://localhost:44327/Account/CreateCourses',
            type: 'post',
            dataType: 'json',
            success: function (data) {
                location.reload();
            },
            data: courses
        });
    }
    else {
        alert("Data entered incorrectly !!!");
    }
}