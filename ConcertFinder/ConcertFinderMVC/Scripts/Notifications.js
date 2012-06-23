$(document).ready(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            var id = $(".notif:last").attr("id");
            $.ajax({
                url: "/Account/GetNextNotifications",
                data: { last_id: id },
                success: function (data) {
                    var url = document.URL.split("//");
                    url = (url[1] ? url[1] : url[0]).split("/");
                    var txt = "";
                    for (var i = 0; i < data.length; i++) {

                        var milli = data[i].Date.replace(/\/Date\((-?\d+)\)\//, '$1');
                        var d = new Date(parseInt(milli));

                        txt += "<li class='notif' id='" + data[i].Id + "'>" +
                                        "<strong>" + data[i].Titre + "</strong>" +
                                        "<span>" + dateFormat(d, "dd/mm/yyyy HH:MM:ss") + "</span>" +
                                        "<p>" + data[i].Message + "</p>";

                        if (data[i].Titre != "Statut") {
                            txt += "<a href='http://" + url[0] + "/Event/Detail/" + data[i].IdEvent + "'>Voir l'événement</a>";
                        }
                        txt += "</li>";
                    }
                    $("#notifs").append(txt);
                },
                dataType: "json",
                error: function (a, b, c) {
                    alert(a + b + c);
                }
            });
        }
    });
});