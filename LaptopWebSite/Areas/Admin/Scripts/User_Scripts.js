TemlateUser();
function TemlateUser() {
    $.ajax({
        url: '/Admin/AdminPanel/ListUser',
        type: 'get',
        dataType: 'json',
        success: function (data) {
            for (var i = 0; i < data.Users.length; i++) {

                var ClassStatus = "'";
                if (data.Users[i].Role == "Admin") {
                    ClassStatus += "label label-sm label-danger'";
                }
                else if (data.Users[i].Role == "User") {
                    ClassStatus += "label label-sm label-info'";
                }

                var t = $('#sample_1_2').DataTable();
                t.row.add([
                    "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input id='" + data.Users[i].Id+"' type='checkbox' class='checkboxes' value='1'><span></span></label>",
                    data.Users[i].Id,
                    data.Users[i].Name,
                    data.Users[i].Email,
                    "<span class=" + ClassStatus + ">" + data.Users[i].Role + "</span >"
                ]).draw(false);
            }
        }
    });
}

function deleteSelectedUser() {
    if (confirm("Are you sure you want to delete these users ?")) {

        var array  = $('.checkboxes:checked');
        for (var i = 0; i < array.length; i++) {

            var model = {
                Id: array[i].id
            };

            $.ajax({
                url: '/Admin/AdminPanel/DeleteUser',
                type: 'post',
                dataType: 'json',
                success: function (data) {
                },
                data: model
            });
        }
            
        location.reload();
    } 
}



