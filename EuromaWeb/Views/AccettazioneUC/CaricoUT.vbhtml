<div>
    <canvas id="bar-chart" width="800" height="450"></canvas>
</div>

<script defer>
    // Bar chart
    var data = JSON.parse("@ViewBag.CaricoOrdiniUT".replace(/&quot;/g, '"'));
    var ctx = document.getElementById("bar-chart").getContext('2d');
    showCarico(ctx, data);
</script>