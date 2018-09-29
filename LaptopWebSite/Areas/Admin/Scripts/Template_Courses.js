TemlateCourses();
function TemlateCourses() {
    $.ajax({
        url: '/Account/ListCourses',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            for (var i = 0; i < data.Courses.length; i++)
            {

                var t = $('#sample_1_2').DataTable();
                t.row.add([
                    "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' value='1'><span></span></label>",
                    data.Courses[i].Name,
                    data.Courses[i].StudyDate,
                    data.Courses[i].Description,
                ]).draw(false);
            }
        }
    });
}




