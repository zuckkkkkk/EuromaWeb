<script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.4/index.global.min.js'></script>
<style>
    #calendar{
        color: black!important;
        text-decoration: none!important;
    }
    .mainContainerCalendar{
        width:100%;
        display: flex;
        justify-content: center!important;
        align-items: center!important;
        align-content: center!important;
    }
    .containerCalendario{
        width:600px;
    }
</style>
<div class="Calendario">
    <h2 style="margin-top: 1rem; margin-bottom: 1rem;">Calendario (Carico Progetti UT)</h2>
    <div class="mainContainerCalendar">
        <div class="containerCalendario">
            <div id='calendar'></div>
        </div>
    </div>
</div>

<script>
    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            aspectRatio: 2,
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            weekends: false,
            events: @Html.Raw(ViewBag.result),
        });
        calendar.setOption('aspectRatio', 1)
        calendar.render();
    });
</script>