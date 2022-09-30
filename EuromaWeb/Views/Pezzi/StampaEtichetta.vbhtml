@Code
    ViewData("Title") = "StampEtichetta"
End Code
<style>
     @@font-face {
        font-family: "Barcode";
        src: url("../../Content/font/Barcode.ttf") format('truetype');
    }
    .etichetta{
        min-width: 336px;
        height: 336px;
        transform: rotate(90deg);
        transform-origin: left top;
    }

    .child-1, .child-2{
        height: 60px;
        width: 34%;
        /* */
    float: left;

    }
    .child-1 {
        margin-left: 16px;
        padding: 20px 0px;
        padding-left: 10px;
        padding-right: 5px;
        overflow: hidden;
    }
    .child-2 {
        font-family: "Barcode";
        padding: 20px 0px;
    }
    .etichettta-container{
        display: inline-block;
        position: absolute;
        top: 0px;
        left: 108px;
    }
</style>
<div class="etichettta-container">
    <div class="etichetta">
        <div class="child-1">
            <h3 style="margin: 0!important;">
                @ViewBag.art
            </h3>
            <p style="font-size: 13px; margin-bottom:4px!important;">
                @ViewBag.desc
            </p>
            <p style="font-size: 13px; margin-top:4px!important;">
                @ViewBag.Qta
            </p>
        </div>
        <div class="child-2">
            <img src="http://bwipjs-api.metafloor.com/?bcid=code128&text=@ViewBag.art" height="64px" />
        </div>

    </div>
</div>

<script>
    window.print();
</script>