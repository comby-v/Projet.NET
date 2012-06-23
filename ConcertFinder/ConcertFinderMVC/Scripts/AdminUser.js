$(document).ready(function () {
    $("select").change(function () {
        var select_id = "#" + $(this).attr("id");
        var new_role = $(select_id + " option:selected").text();
        var splitted = select_id.split('_');
        var id_user = splitted[1];

        $.post("/Admin/ChangeRole", { id: id_user, role: new_role });
    });

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            var url = document.URL.split("//");
            url = (url[1] ? url[1] : url[0]).split("/");
            var id = $("#users tr:last").attr("id");
            $.ajax({
                url: "http://" + url[0] + "/Admin/GetNextUsers", //"http://"+url[0]+"/ConcertFinderMVC/Admin/GetNextUsers",
                data: { last_id: id },
                success: function (data) {
                    var text = "";
                    for (var i = 0; i < data.length; i++) {
                        text += "<tr id='" + data[i].Id + "'>" +
                                    "<td>" + data[i].Name + "</td>" +
                                    "<td>" + data[i].FirstName + "</td>" +
                                    "<td>" + data[i].Login + "</td>" +
                                    "<td>" + data[i].Mail + "</td>" +
                                    "<td>" + data[i].City + "</td>" +
                                    "<td>" +
                                        "<select>" +
                                            "<option>" + data[i].Role + "</option>" +
                                            "<option>Moderateur</option>" +
                                            "<option>Administrateur</option>" +
                                        "</select>" +
                                    "</td>" +
                                    "<td><a href='http://" + url[0] + "/Admin/BlockUser/" + data[i].Id + "'>Supprimer</a></td>" +
                                "</tr>";
                    }
                    $("#users").append(text);
                },
                dataType: "json"
            });
        }
    });
});