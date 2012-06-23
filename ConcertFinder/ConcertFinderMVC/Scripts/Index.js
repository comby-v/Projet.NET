$(document).ready(function () {
    setInterval(function (){
        var url = document.URL.split("//");
        url = (url[1] ? url[1] : url[0]).split("/");
        var qtype = "";
        if (url.length > 2)
        {
            qtype = url[2];
        }
        var id = $("#item_lastadded li:first").attr("id");
        $.ajax({
            url: "http://" + url[0] + "/Event/Refresh",
            data: { first_id : id, type : qtype },
            success: function (data){
                for (var i = 0; i < data.length; i++) {
                    var milli = data[i].StartDate.replace(/\/Date\((-?\d+)\)\//, '$1');
                    var d = new Date(parseInt(milli));
                    var text = "<li>" +
                        "<p class='quick_title'><a href='/Event/Detail/"+data[i].Id+"'>"+data[i].Titre+"</a></p>" +
                        "<p class='quick_infos'>A "+data[i].Ville+", le "+dateFormat(d, "dd/mm/yyyy HH:MM:ss")+"</p>" +
                    "</li>";
                    $("#item_lastadded").prepend(text);
                }
            },
            dataType: "json"
        });
    }, 20000);

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            var url = document.URL.split("//");
            url = (url[1] ? url[1] : url[0]).split("/");
            var qtype = "";
            if (url.length > 2)
            {
                qtype = url[2];
            }
            var id = $(".item:last").attr("id");
            $.ajax({
                url: "http://"+url[0]+"/Event/GetNextEvents",//"http://"+url[0]+"/ConcertFinderMVC/Event/GetNextEvents",
                data: { last_id: id, type : qtype },
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {

                        var text = "<li class='item' id='"+ data[i].Id +"'>" +
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
                },
                dataType: "json"
            });
        }
    });
});