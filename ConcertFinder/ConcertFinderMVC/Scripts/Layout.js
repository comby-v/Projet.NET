$(document).ready(function () {
        $("#open_connect").click(function () {
            $("#connect_panel").toggle('blind', null, 500);
        });

        setInterval(function (){
            var url = document.URL.split("//");
            url = (url[1] ? url[1] : url[0]).split("/");
            $.ajax({
                url: "http://" + url[0] + "/Account/Check",
                data: { user : "@User.Identity.Name" },
                success: function (data){
                    if (parseInt(data) != -1){
                        $("#Mynotifs").text(" ("+data+")");
                    }
                }
            });
        }, 45000);

        $("#SearchBar").keyup(function () {
            var url = document.URL.split("//");
            url = (url[1] ? url[1] : url[0]).split("/");
            if ($(this).val().length >= 2) {
                $.ajax({
                    url: "http://" + url[0] + "/Event/Search", //"http://" + url[0] + "/ConcertFinderMVC/Event/Search"
                    data: { q: $(this).val() },
                    success: function (data) {
                        if ($("#items") != null) {
                            $("#items").children("li").remove();
                            for (var i = 0; i < data.length; i++) {

                                var text = "<li class='item' id='"+ data[i].Id +"'>"+
                                                "<a href='/Event/Detail/" + data[i].Id + "'>";
                                if (data[i].Image != null) {
                                    // text += "<img src="+"@@Url.Content("/ConcertFinderMVC/")"+data[i].Image+" width='125' height='160' alt='flyers' class='flyers float-left' />";
                                    text += "<img src='/'"+data[i].Image+" width='125' height='160' alt='flyers' class='flyers float-left' />";
                                }
                                else {
                                    text += "<img src='/Content/Images/no_avatar.gif' width='125' height='160' alt='flyers' class='flyers float-left' />";
                                }

                                var milli = data[i].StartDate.replace(/\/Date\((-?\d+)\)\//, '$1');
                                var d = new Date(parseInt(milli));

                                text += "<strong class='item_titre'>" + data[i].Titre + "</strong>" +
                                    "<span class='item_type float-left'>" + data[i].Type + "</span>" +
                                    "<span class='item_Date float-right'>" + dateFormat(d, "dd/mm/yyyy") + " à " + data[i].Ville + " (" + data[i].CP + ")</span>" +
                                    "<span class='item_salle'>" + data[i].Salle + "</span>"+
                                    "<em class='item_descr'>" +
                                        data[i].Description +
                                    "</em>" +
                                "</a>" +
                                "</li>";

                                $("#items").append(text);
                            }
                        }
                    },
                    dataType: "json"
                });
            }
        });

    });