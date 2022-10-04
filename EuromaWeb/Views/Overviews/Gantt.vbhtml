<div class="container-gantt">
    <div class="gantt-target"></div>
</div>
<script>
    $(function () {
        "use strict";

     
     
        $.ajax({
            method: "POST",
            url: '@Url.Action("PostGantt", "Overviews", New With {.id = ViewBag.idOP})',
            success: function (data) {
                if (data.ok) {
                    $('.gantt-target').remove();
                    $(".container-gantt").append("<div class='gantt-target'></div>")

                    console.log(data);
                    new Gantt(".gantt-target", data.lista, {
                        on_click: function (task) {
                            console.log(task);
                        },
                        on_date_change: function (task, start, end) {
                            console.log(task, start, end);
                        },
                        on_progress_change: function (task, progress) {
                            console.log(task, progress);
                        },
                        on_view_change: function (mode) {
                            console.log(mode);
                        },
                        view_mode: 'Day',
                        language: 'en'
                    });
                    setTimeout(function () {
                        $(".today-highlight")[0].scrollIntoView({
                            behavior: 'smooth',
                            block: 'start',
                            inline: 'start'
                        });
                    }, 500);
                   
                };
            },
            error: function (error_data) {
                console.log(error_data);
                console.log("Endpoint request error");
            }
        });


        //$(".gantt").popover({
        //    selector: ".bar",
        //    title: function _getItemText() {
        //        return this.textContent;
        //    },
        //    container: '.gantt',
        //    content: "Here's some useful information.",
        //    trigger: "hover",
        //    placement: "auto right"
        //});
    });

</script>

