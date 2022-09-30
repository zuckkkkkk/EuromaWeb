@ModelType IEnumerable(Of EuromaWeb.Overview)
@Code
    ViewData("Title") = "Index"
End Code
<style>
    .donut-inner {
        margin-top: -100px;
        margin-bottom: 100px;
    }

        .donut-inner h5 {
            margin-bottom: 5px;
            margin-top: 0;
        }

        .donut-inner span {
            font-size: 12px;
        }
    .cls-1, .cls-10, .cls-23, .cls-24, .cls-4, .cls-6, .cls-9 {
        fill: none;
    }

    .cls-2 {
        isolation: isolate;
    }

    .cls-3 {
        clip-path: url(#clip-path);
    }

    .cls-4, .cls-6 {
        stroke: #000;
    }

    .cls-4 {
        stroke-linecap: round;
        stroke-linejoin: round;
        stroke-width: 0.5px;
    }

    .cls-5 {
        fill: #fff;
    }

    .cls-10, .cls-23, .cls-24, .cls-6, .cls-9 {
        stroke-miterlimit: 10;
        stroke-width: 2px;
    }

    .cls-23, .cls-6, .cls-9 {
        fill-rule: evenodd;
    }

    .cls-11, .cls-22, .cls-7 {
        font-size: 22.09px;
    }

    .cls-11, .cls-13, .cls-14, .cls-17, .cls-22, .cls-7 {
        font-family: CeraPro-MediumItalic, Cera Pro;
        font-weight: 500;
        font-style: italic;
    }

    .cls-8 {
        font-size: 72.69px;
    }

    .cls-16, .cls-8 {
        font-family: MyriadPro-Regular, Myriad Pro;
    }

    .cls-10, .cls-9 {
        stroke: #00ef43;
    }

    .cls-11, .cls-17 {
        fill: #00ef43;
    }

    .cls-12 {
        letter-spacing: -0.03em;
    }

    .cls-13 {
        font-size: 37.72px;
    }

    .cls-14 {
        font-size: 32.07px;
    }

    .cls-15 {
        letter-spacing: 0em;
    }

    .cls-16 {
        font-size: 69.84px;
    }

    .cls-17 {
        font-size: 21.23px;
    }

    .cls-18 {
        letter-spacing: -0.03em;
    }

    .cls-19 {
        letter-spacing: -0.08em;
    }

    .cls-20 {
        letter-spacing: -0.03em;
    }

    .cls-21 {
        letter-spacing: -0.01em;
    }

    .cls-22 {
        fill: #e00000;
    }

    .cls-23, .cls-24 {
        stroke: #e00000;
    }
    .title_ov {
        font-size: 64px;
        text-align: center;
    }
    .shadow {
        box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
        border-radius: 15px;
    }
    .container-graphs {
        display: flex;
        align-items: center;
        justify-content: center;
        margin: 16px;
        min-width: 400px;
    }
    .Chart{
        margin: 16px;
        margin-top: 64px;
    }
    .main-card-title{
        position: absolute;
        margin: 0 auto;
        text-align: center;
        width: 100%;
        margin-top: -50px;
        font-size: 40px;
    }
    .container {
        width: 90vw;
    }
    .ct{
        display: flex;
        justify-content:center;
    }
    canvas {
        box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
        margin: 8px;
        border-radius: 16px;
    }
    svg {
        width: 70%;
    }
    .mainOverview {
        display: flex;
        justify-content: center;
        text-align: center;
        width: 75vw;
    }
</style>
<div class="row">
    <div class="col-md-4">
        <div class="row text-center" style="font-weight: 700; font-size: 40px; margin-top: 3rem;">
            Produzione
        </div>
        <div class="row " style="font-weight: 300; font-size: 20px;">
            Ore uomo <br /> @ViewBag.StartDate.ToString.Split(" ")(0) • @ViewBag.EndDate.ToString.Split(" ")(0)
        </div>
    </div>
    <div class="col-md-8">
        <div class="mainOverview d-block" @*style="display: none;"*@>
            <svg id="Livello_1" data-name="Livello 1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 1366.88 1140.35">
                <defs>
                    <clipPath id="clip-path" transform="translate(-171.19 -54.67)"><rect class="cls-1" x="19.16" y="-7.96" width="1684" height="1191" /></clipPath>
                </defs>
                <g class="cls-2"><g class="cls-3"><rect class="cls-4" x="1037.55" y="501.64" width="13.01" height="10.46" /><polyline class="cls-4" points="911.2 474.84 945.12 474.75 945.12 477.74" /><line class="cls-4" x1="911.2" y1="471.06" x2="909.35" y2="471.06" /><line class="cls-4" x1="906.63" y1="465.87" x2="909.35" y2="465.87" /><polyline class="cls-4" points="909.35 470.88 906.63 470.88 906.63 465.87" /><polyline class="cls-4" points="909.44 409.02 909.44 406.3 914.54 406.3" /><line class="cls-4" x1="909.35" y1="409.02" x2="909.35" y2="418.07" /><polyline class="cls-4" points="932.73 407.88 921.65 407.88 921.65 400.15" /><line class="cls-4" x1="908.03" y1="470.88" x2="909.35" y2="470.88" /><line class="cls-4" x1="909.44" y1="407.7" x2="909.44" y2="409.02" /><line class="cls-4" x1="914.54" y1="406.3" x2="914.54" y2="409.02" /><polyline class="cls-4" points="940.46 382.75 908.74 382.75 908.74 400.15 921.65 400.15" /><line class="cls-4" x1="911.2" y1="474.84" x2="911.2" y2="471.06" /><path class="cls-4" d="M1080.54,472.74v53" transform="translate(-171.19 -54.67)" /><line class="cls-4" x1="932.73" y1="409.02" x2="909.35" y2="409.02" /><polyline class="cls-4" points="1040.72 400.59 1040.72 406.74 1040.72 400.59 1075.87 400.59" /><line class="cls-4" x1="1044.23" y1="487.23" x2="1075.87" y2="487.23" /><line class="cls-4" x1="1079.91" y1="400.59" x2="1075.87" y2="400.59" /><line class="cls-4" x1="1075.87" y1="487.23" x2="1079.91" y2="487.23" /><line class="cls-4" x1="1044.23" y1="487.14" x2="1044.23" y2="487.05" /><line class="cls-4" x1="1079.91" y1="409.81" x2="1088.52" y2="409.81" /><polyline class="cls-4" points="1120.77 386.79 1090.01 386.79 1090.01 400.23" /><polyline class="cls-4" points="1120.77 386.79 1120.77 401.64 1122.09 401.64" /><line class="cls-4" x1="1088.7" y1="506.56" x2="1088.7" y2="506.47" /><rect class="cls-4" x="1075.51" y="384.07" width="14.41" height="14.41" /><line class="cls-4" x1="1122.09" y1="401.64" x2="1122.09" y2="386.26" /><rect class="cls-4" x="1228.85" y="420.97" width="1.23" height="49.82" /><line class="cls-4" x1="1229.9" y1="419.83" x2="1194.67" y2="384.59" /><line class="cls-4" x1="1193.88" y1="385.47" x2="1229.02" y2="420.62" /><polyline class="cls-4" points="1228.76 471.14 1229.55 471.94 1195.37 506.12" /><polyline class="cls-4" points="1228.76 471.14 1194.58 505.33 1195.37 506.12" /><line class="cls-4" x1="1229.02" y1="420.62" x2="1229.9" y2="419.83" /><line class="cls-4" x1="1193.88" y1="385.47" x2="1194.67" y2="384.59" /><line class="cls-4" x1="1088.87" y1="506.56" x2="1141.42" y2="506.56" /><line class="cls-4" x1="1141.42" y1="505.41" x2="1088.87" y2="505.41" /><polyline class="cls-4" points="1194.23 505.42 1194.23 506.56 1141.68 506.56" /><polyline class="cls-4" points="1194.23 505.42 1141.68 505.42 1141.68 506.56" /><line class="cls-4" x1="1088.87" y1="506.56" x2="1088.87" y2="505.41" /><line class="cls-4" x1="1141.42" y1="506.56" x2="1141.42" y2="505.41" /><polyline class="cls-4" points="1088.7 506.47 1088.7 485.82 1079.91 485.82" /><line class="cls-4" x1="1193.52" y1="384.15" x2="1128.32" y2="384.15" /><line class="cls-4" x1="1129.29" y1="384.07" x2="1129.29" y2="384.15" /><polyline class="cls-4" points="1193.53 384.15 1193.53 385.3 1128.33 385.3 1128.33 384.15" /><polyline class="cls-4" points="1129.29 383.98 1127.97 383.98 1122.09 386.26" /><line class="cls-4" x1="932.73" y1="400.59" x2="932.73" y2="409.02" /><line class="cls-4" x1="1079.91" y1="409.81" x2="1079.91" y2="400.59" /><line class="cls-4" x1="1040.72" y1="406.74" x2="993.53" y2="420.62" /><polyline class="cls-4" points="991.25 418.33 991.25 413.42 988.52 410.69 988.52 400.59 986.85 400.59" /><polyline class="cls-4" points="1052.49 514.64 1071.3 514.64 1071.3 501.64" /><polyline class="cls-4" points="1052.49 514.64 1052.49 501.64 1071.3 501.64" /><line class="cls-4" x1="993.53" y1="420.62" x2="991.25" y2="418.34" /><line class="cls-4" x1="940.46" y1="400.59" x2="940.46" y2="376.77" /><polyline class="cls-4" points="941.16 400.59 941.16 376.77 940.46 376.77" /><line class="cls-4" x1="987.47" y1="400.59" x2="987.47" y2="376.77" /><polyline class="cls-4" points="988.09 400.59 988.09 376.77 987.47 376.77" /><line class="cls-4" x1="988.09" y1="400.59" x2="932.73" y2="400.59" /><line class="cls-4" x1="1079.91" y1="400.59" x2="1040.72" y2="400.59" /><line class="cls-4" x1="1079.91" y1="487.23" x2="1079.91" y2="485.82" /><path class="cls-4" d="M1135.46,455.25l-.09-2.37A23.93,23.93,0,0,0,1114,431.53l-2.37-.09" transform="translate(-171.19 -54.67)" /><path class="cls-4" d="M1159.28,431.44l-2.38.09a23.93,23.93,0,0,0-21.35,21.35l-.09,2.37" transform="translate(-171.19 -54.67)" /><polyline class="cls-4" points="1088.52 409.81 1088.52 400.23 1090.01 400.23" /><line class="cls-4" x1="1079.91" y1="409.81" x2="1088.52" y2="409.9" /><polyline class="cls-4" points="909.53 522.37 1043.88 522.37 1043.88 487.23" /><polyline class="cls-4" points="945.12 477.74 909.53 477.74 909.53 522.37" /><line class="cls-4" x1="909.53" y1="477.74" x2="874.29" y2="477.74" /><polyline class="cls-4" points="909.53 522.37 874.29 522.37 874.29 477.74" /><rect class="cls-4" x="335.56" y="260.7" width="60.63" height="129.96" /><rect class="cls-4" x="364.38" y="226.08" width="31.81" height="34.62" /><rect class="cls-4" x="682.82" y="99.02" width="118.36" height="46.13" /><rect class="cls-4" x="791.08" y="122.04" width="49.03" height="31.81" /><rect class="cls-4" x="554.36" y="116.33" width="128.47" height="28.82" /><line class="cls-4" x1="767.97" y1="252.79" x2="767.97" y2="237.94" /><line class="cls-4" x1="791.08" y1="166.59" x2="840.11" y2="166.59" /><polyline class="cls-4" points="791.08 313.86 791.08 267.64 819.81 267.64" /><polyline class="cls-4" points="822.8 267.64 822.8 313.86 791.08 313.86" /><polyline class="cls-4" points="840.11 166.59 840.11 267.64 791.08 267.64 791.08 166.59" /><line class="cls-4" x1="767.97" y1="237.94" x2="791.08" y2="237.94" /><line class="cls-4" x1="767.97" y1="252.79" x2="791.08" y2="252.79" /><rect class="cls-4" x="670.87" y="267.64" width="93.05" height="50.44" /><rect class="cls-4" x="632.83" y="267.64" width="38.05" height="28.73" /><rect class="cls-4" x="743.72" y="323.88" width="20.21" height="17.31" /><line class="cls-4" x1="753.82" y1="323.88" x2="753.82" y2="318.08" /><rect class="cls-4" x="349.97" y="99.02" width="118.36" height="51.93" /><rect class="cls-4" x="468.33" y="119.23" width="46.22" height="31.72" /><rect class="cls-4" x="306.65" y="846.26" width="86.64" height="86.55" /><line class="cls-4" x1="772.28" y1="1065.94" x2="772.28" y2="940.46" /><line class="cls-4" x1="773.68" y1="1065.94" x2="773.68" y2="941.78" /><line class="cls-4" x1="772.28" y1="940.46" x2="828.51" y2="940.28" /><line class="cls-4" x1="773.68" y1="941.78" x2="828.51" y2="941.78" /><line class="cls-4" x1="772.28" y1="1025.6" x2="773.68" y2="1025.6" /><line class="cls-4" x1="772.28" y1="982.28" x2="773.68" y2="982.28" /><line class="cls-4" x1="773.68" y1="1002.49" x2="772.28" y2="1002.49" /><path class="cls-4" d="M943.47,1057.86l-2.38.09a22.46,22.46,0,0,0-19.94,20l-.09,2.37" transform="translate(-171.19 -54.67)" /><line class="cls-4" x1="772.28" y1="1025.6" x2="749.78" y2="1025.6" /><line class="cls-4" x1="278.89" y1="830.62" x2="278.89" y2="778.6" /><polyline class="cls-4" points="235.65 830.62 235.65 778.6 278.89 778.6" /><line class="cls-4" x1="278.89" y1="830.62" x2="235.65" y2="830.62" /><rect class="cls-4" x="235.65" y="696.88" width="43.23" height="75.04" /><line class="cls-4" x1="127.57" y1="1015.32" x2="127.57" y2="891.25" /><polyline class="cls-4" points="66.94 891.25 66.94 1015.32 127.57 1015.32" /><line class="cls-4" x1="66.94" y1="891.25" x2="127.57" y2="891.25" /><polyline class="cls-4" points="127.57 995.82 144.88 995.82 144.88 965.5 127.57 965.5" /><polyline class="cls-4" points="221.16 1015.32 290.49 1015.32 290.49 859.44" /><polyline class="cls-4" points="221.16 1015.32 221.16 859.44 290.49 859.44" /><line class="cls-4" x1="221.15" y1="969.63" x2="217.02" y2="969.63" /><rect class="cls-4" x="198.13" y="961.37" width="18.89" height="16.43" /><polyline class="cls-4" points="153.58 775.7 153.58 746.88 66.94 746.88" /><line class="cls-4" x1="153.58" y1="775.7" x2="127.57" y2="775.7" /><polyline class="cls-4" points="66.94 746.88 66.94 862.34 127.57 862.34 127.57 775.7" /><line class="cls-4" x1="790.11" y1="286.35" x2="811.03" y2="286.35" /><line class="cls-4" x1="813.22" y1="286.35" x2="817.53" y2="286.35" /><line class="cls-4" x1="819.73" y1="286.35" x2="837.04" y2="286.35" /><line class="cls-4" x1="839.23" y1="286.35" x2="843.54" y2="286.35" /><line class="cls-4" x1="845.65" y1="286.35" x2="866.65" y2="286.35" /><line class="cls-4" x1="828.34" y1="324.67" x2="828.34" y2="303.66" /><line class="cls-4" x1="828.34" y1="301.47" x2="828.34" y2="297.16" /><line class="cls-4" x1="828.34" y1="295.05" x2="828.34" y2="277.66" /><line class="cls-4" x1="828.34" y1="275.55" x2="828.34" y2="271.15" /><line class="cls-4" x1="828.34" y1="269.04" x2="828.34" y2="248.04" /><rect class="cls-4" x="824.03" y="282.05" width="8.7" height="8.61" /><line class="cls-4" x1="832.47" y1="282.05" x2="832.73" y2="282.22" /><line class="cls-4" x1="831.68" y1="282.05" x2="832.73" y2="283.02" /><line class="cls-4" x1="830.88" y1="282.05" x2="832.73" y2="283.89" /><line class="cls-4" x1="830.01" y1="282.05" x2="832.73" y2="284.68" /><line class="cls-4" x1="829.22" y1="282.05" x2="832.73" y2="285.48" /><line class="cls-4" x1="828.42" y1="282.05" x2="832.73" y2="286.35" /><line class="cls-4" x1="827.63" y1="282.05" x2="832.73" y2="287.15" /><line class="cls-4" x1="826.75" y1="282.05" x2="832.73" y2="287.94" /><line class="cls-4" x1="825.96" y1="282.05" x2="832.73" y2="288.73" /><line class="cls-4" x1="825.17" y1="282.05" x2="832.73" y2="289.61" /><line class="cls-4" x1="824.29" y1="282.05" x2="832.73" y2="290.4" /><line class="cls-4" x1="824.03" y1="282.58" x2="832.2" y2="290.66" /><line class="cls-4" x1="824.03" y1="283.37" x2="831.32" y2="290.66" /><line class="cls-4" x1="824.03" y1="284.16" x2="830.53" y2="290.66" /><line class="cls-4" x1="824.03" y1="285.04" x2="829.74" y2="290.66" /><line class="cls-4" x1="824.03" y1="285.83" x2="828.86" y2="290.66" /><line class="cls-4" x1="824.03" y1="286.62" x2="828.07" y2="290.66" /><line class="cls-4" x1="824.03" y1="287.41" x2="827.28" y2="290.66" /><line class="cls-4" x1="824.03" y1="288.29" x2="826.49" y2="290.66" /><line class="cls-4" x1="824.03" y1="289.08" x2="825.61" y2="290.66" /><line class="cls-4" x1="824.03" y1="289.87" x2="824.82" y2="290.66" /><rect class="cls-4" x="83.46" y="546.27" width="28.91" height="28.91" /><rect class="cls-4" x="580.63" y="576.5" width="475.82" height="28.91" /><line class="cls-4" x1="840.29" y1="1048.36" x2="825.53" y2="1048.36" /><line class="cls-4" x1="837.3" y1="915.68" x2="828.51" y2="915.68" /><line class="cls-4" x1="825.53" y1="1048.36" x2="825.53" y2="1065.76" /><line class="cls-4" x1="941.25" y1="1065.76" x2="302" y2="1065.76" /><polyline class="cls-4" points="420.97 1065.76 420.97 961.72 306.65 961.72 306.65 576.41 320.98 576.41 320.98 570 389.78 570 389.78 565.69 321.06 565.69 321.06 558.93 306.3 558.93 306.3 330.64 321.24 330.64 321.24 316.32 306.65 316.32 306.65 87.5 320.89 87.5 320.89 70.28 515.43 70.28 515.43 63.43" /><line class="cls-4" x1="39.35" y1="63.43" x2="1355.03" y2="63.43" /><polyline class="cls-4" points="1340.27 70.11 558.75 70.11 558.75 63.43" /><polyline class="cls-4" points="580.63 87.33 566.22 87.33 566.22 70.11" /><line class="cls-4" x1="580.63" y1="87.33" x2="580.63" y2="70.11" /><polyline class="cls-4" points="840.11 87.33 825.7 87.33 825.7 70.11" /><line class="cls-4" x1="840.11" y1="87.33" x2="840.11" y2="70.11" /><polyline class="cls-4" points="1099.68 87.33 1085.27 87.33 1085.27 70.11" /><line class="cls-4" x1="1099.68" y1="87.33" x2="1099.68" y2="70.11" /><polyline class="cls-4" points="1345.63 70.63 1345.63 139.17 1358.55 139.17 1358.55 142.42 1345.19 142.42 1345.19 175.9 1358.02 175.9 1358.02 180.12 1345.71 180.12 1345.71 330.73 1349.58 330.73 1349.58 387.14 1345.8 387.14 1345.8 384.42 1267.42 384.42 1267.42 565.69 1346.15 565.69" /><polyline class="cls-4" points="320.71 820.43 320.71 805.32 306.65 805.32" /><line class="cls-4" x1="320.71" y1="820.43" x2="306.65" y2="820.43" /><line class="cls-4" x1="1085.18" y1="904.61" x2="1085.18" y2="921.92" /><line class="cls-4" x1="1085.18" y1="945.03" x2="1085.18" y2="992.83" /><polyline class="cls-4" points="1085.18 1047.66 1085.18 1065.94 984.22 1065.94 984.22 1072.53" /><line class="cls-4" x1="837.3" y1="915.68" x2="837.3" y2="1048.28" /><line class="cls-4" x1="840.29" y1="1048.36" x2="840.29" y2="1065.59" /><line class="cls-4" x1="941.25" y1="1065.76" x2="941.25" y2="1073.23" /><line class="cls-4" x1="1242.82" y1="912.52" x2="1242.82" y2="904.52" /><line class="cls-4" x1="1199.68" y1="912.52" x2="1199.68" y2="904.52" /><polyline class="cls-4" points="1242.82 912.52 1340.36 912.52 1340.36 1065.5 1099.94 1065.5" /><line class="cls-4" x1="1196.16" y1="1065.5" x2="1196.16" y2="912.52" /><polyline class="cls-4" points="1366.63 1073.06 39.35 1073.06 39.35 63.43" /><polyline class="cls-4" points="82.06 63.43 82.06 70.28 46.73 70.28 46.73 596.18" /><polyline class="cls-4" points="46.73 630.8 46.73 1065.41 82.32 1065.41 82.32 1073.06" /><polyline class="cls-4" points="61.41 1065.41 61.41 1048.19 46.73 1048.19" /><line class="cls-4" x1="61.41" y1="819.99" x2="46.73" y2="819.99" /><polyline class="cls-4" points="46.73 805.32 61.41 805.32 61.41 819.99" /><line class="cls-4" x1="61.41" y1="576.24" x2="46.73" y2="576.24" /><polyline class="cls-4" points="46.73 558.66 61.41 558.66 61.41 576.24" /><line class="cls-4" x1="61.41" y1="330.82" x2="46.73" y2="330.82" /><polyline class="cls-4" points="46.73 316.06 61.41 316.06 61.41 330.82" /><polyline class="cls-4" points="298.83 63.43 298.83 70.37 302 70.37 302 1065.76 298.57 1065.76 298.57 1073.06" /><polygon class="cls-4" points="311.22 1019.45 417.72 1019.45 417.72 964.71 311.05 964.71 311.22 1019.45" /><line class="cls-4" x1="389.43" y1="419.48" x2="389.43" y2="565.69" /><polyline class="cls-4" points="386.35 565.69 386.35 506.03 389.43 506.03" /><polyline class="cls-4" points="389.43 480.55 386.35 480.55 386.35 422.64 306.3 422.64" /><line class="cls-4" x1="389.43" y1="419.48" x2="306.3" y2="419.48" /><line class="cls-4" x1="494.25" y1="569.91" x2="389.78" y2="570" /><line class="cls-4" x1="441.88" y1="596.36" x2="441.88" y2="533.8" /><line class="cls-4" x1="333.01" y1="638.54" x2="270.45" y2="638.54" /><line class="cls-4" x1="306.65" y1="595.22" x2="302" y2="595.22" /><line class="cls-4" x1="306.65" y1="681.86" x2="302" y2="681.86" /><line class="cls-4" x1="68.52" y1="613.49" x2="0.25" y2="613.49" /><line class="cls-4" x1="46.73" y1="596.18" x2="39.35" y2="596.18" /><line class="cls-4" x1="46.73" y1="630.8" x2="39.35" y2="630.8" /><line class="cls-4" x1="189.87" y1="114.22" x2="189.87" y2="0.25" /><polyline class="cls-4" points="46.73 87.42 61.49 87.42 61.49 70.28" /><line class="cls-4" x1="190.14" y1="1140.1" x2="190.14" y2="1010.14" /><line class="cls-4" x1="580.72" y1="1048.1" x2="580.72" y2="1065.76" /><polyline class="cls-4" points="565.87 1065.76 565.87 1048.1 580.72 1048.1" /><line class="cls-4" x1="962.69" y1="1109.87" x2="962.69" y2="1036.06" /><line class="cls-4" x1="1093.62" y1="1047.66" x2="1085.18" y2="1047.66" /><line class="cls-4" x1="1093.62" y1="992.83" x2="1085.18" y2="992.83" /><line class="cls-4" x1="1093.62" y1="918.4" x2="1093.62" y2="921.92" /><line class="cls-4" x1="1093.62" y1="945.03" x2="1093.62" y2="966.64" /><line class="cls-4" x1="1093.62" y1="969.81" x2="1093.62" y2="992.83" /><polyline class="cls-4" points="1093.62 1047.66 1099.94 1047.66 1099.94 1065.5" /><line class="cls-4" x1="1191.15" y1="1065.5" x2="1191.15" y2="969.81" /><line class="cls-4" x1="1191.15" y1="966.64" x2="1191.15" y2="912.52" /><polyline class="cls-4" points="1199.68 912.52 1100.03 912.52 1100.03 918.4 1093.62 918.4" /><line class="cls-4" x1="1220.5" y1="942.83" x2="1220.5" y2="881.41" /><line class="cls-4" x1="1345.63" y1="783" x2="1310.57" y2="783" /><line class="cls-4" x1="1345.63" y1="791.61" x2="1310.57" y2="791.61" /><line class="cls-4" x1="1345.63" y1="800.22" x2="1310.57" y2="800.22" /><line class="cls-4" x1="1345.63" y1="809.27" x2="1310.57" y2="809.27" /><line class="cls-4" x1="1345.63" y1="817.7" x2="1310.57" y2="817.7" /><line class="cls-4" x1="1345.63" y1="826.05" x2="1310.57" y2="826.05" /><line class="cls-4" x1="1345.63" y1="834.93" x2="1310.57" y2="834.93" /><line class="cls-4" x1="1345.63" y1="843.54" x2="1310.57" y2="843.54" /><line class="cls-4" x1="1345.63" y1="852.32" x2="1310.57" y2="852.32" /><line class="cls-4" x1="1345.63" y1="861.82" x2="1310.57" y2="861.82" /><polyline class="cls-4" points="1345.63 869.72 1310.57 869.72 1310.57 904.52" /><line class="cls-4" x1="1302.75" y1="869.72" x2="1302.75" y2="904.52" /><line class="cls-4" x1="1293.35" y1="869.72" x2="1293.35" y2="904.52" /><line class="cls-4" x1="1284.82" y1="869.72" x2="1284.82" y2="904.52" /><line class="cls-4" x1="1275.95" y1="869.72" x2="1275.95" y2="887.21" /><polyline class="cls-4" points="415 1022.53 417.9 1022.53 417.9 1062.86 374.66 1062.86 374.66 1022.53 392.24 1022.53" /><line class="cls-4" x1="415" y1="1022.53" x2="415" y2="1019.45" /><line class="cls-4" x1="392.24" y1="1022.53" x2="392.24" y2="1019.45" /><line class="cls-4" x1="358.06" y1="1022.53" x2="358.06" y2="1019.45" /><line class="cls-4" x1="334.68" y1="1022.44" x2="334.68" y2="1019.45" /><polyline class="cls-4" points="358.06 1022.53 371.68 1022.53 371.68 1062.86 321.15 1062.86 321.15 1048.01 310.87 1048.01 310.87 1022.44 334.68 1022.44" /><line class="cls-4" x1="346.81" y1="1034.74" x2="346.81" y2="1010.4" /><line class="cls-4" x1="403.4" y1="1034.74" x2="403.4" y2="1010.4" /><line class="cls-4" x1="869.02" y1="96.2" x2="869.02" y2="19.23" /><line class="cls-4" x1="537.93" y1="96.2" x2="537.93" y2="18.35" /><line class="cls-4" x1="846.53" y1="70.28" x2="846.53" y2="63.43" /><line class="cls-4" x1="889.93" y1="70.11" x2="889.93" y2="63.43" /><polyline class="cls-4" points="1340.27 70.11 1340.27 376.51 1258.9 376.51 1258.9 565.69 1099.86 565.69 1099.86 559.01 1085.27 559.01 1085.27 565.61 840.2 565.61 840.2 558.93 825.52 558.93 825.52 565.61 580.81 565.61 580.81 558.93 566.13 558.93 566.13 565.52 494.25 565.52 494.25 569.91 565.96 569.91 565.96 576.5 580.63 576.5 580.63 570.17 825.52 570.17 825.52 576.5 840.29 576.5 840.29 570.17 1085.27 570.17 1085.27 576.41 1099.94 576.41 1099.94 570.17 1345.63 570.17 1345.54 904.52" /><polyline class="cls-4" points="1345.63 773.95 1310.57 773.95 1310.57 772.45 1308.81 772.45 1308.81 867.62 1266.37 867.62 1284.65 904.78" /><line class="cls-4" x1="1345.54" y1="904.52" x2="1085.18" y2="904.61" /><line class="cls-4" x1="417.72" y1="993.62" x2="420.8" y2="993.62" /><line class="cls-4" x1="417.72" y1="970.86" x2="420.8" y2="970.86" /><line class="cls-4" x1="828.51" y1="915.68" x2="828.51" y2="1048.28" /><line class="cls-4" x1="1191.15" y1="969.81" x2="1093.62" y2="969.81" /><line class="cls-4" x1="1093.62" y1="966.64" x2="1191.15" y2="966.64" /><line class="cls-4" x1="1093.62" y1="921.92" x2="1085.18" y2="921.92" /><line class="cls-4" x1="1093.62" y1="945.03" x2="1085.18" y2="945.03" /><line class="cls-4" x1="1085.18" y1="992.83" x2="1085.18" y2="1047.66" /><line class="cls-4" x1="1086.59" y1="992.83" x2="1086.59" y2="1047.66" /><line class="cls-4" x1="1085.09" y1="944.68" x2="1085.09" y2="921.92" /><line class="cls-4" x1="1086.5" y1="944.68" x2="1086.5" y2="921.92" /><line class="cls-4" x1="1085.27" y1="70.11" x2="1085.27" y2="63.43" /><line class="cls-4" x1="320.89" y1="70.28" x2="320.89" y2="63.43" /><line class="cls-4" x1="320.89" y1="66.85" x2="515.43" y2="66.85" /><line class="cls-4" x1="1085.27" y1="66.77" x2="889.93" y2="66.77" /><line class="cls-4" x1="772.28" y1="1065.94" x2="772.28" y2="940.46" /><line class="cls-4" x1="773.68" y1="1065.94" x2="773.68" y2="941.78" /><line class="cls-4" x1="772.28" y1="940.46" x2="828.51" y2="940.28" /><line class="cls-4" x1="773.68" y1="941.78" x2="828.51" y2="941.78" /><line class="cls-4" x1="772.28" y1="1025.6" x2="773.68" y2="1025.6" /><line class="cls-4" x1="772.28" y1="982.28" x2="773.68" y2="982.28" /><line class="cls-4" x1="773.68" y1="1002.49" x2="772.28" y2="1002.49" /><path class="cls-4" d="M943.47,1057.86l-2.38.09a22.46,22.46,0,0,0-19.94,20l-.09,2.37" transform="translate(-171.19 -54.67)" /><line class="cls-4" x1="772.28" y1="1025.6" x2="749.78" y2="1025.6" /><polygon class="cls-4" points="1147.22 898.81 1176.13 898.81 1176.04 691 1147.22 691 1147.22 898.81" /><rect class="cls-4" x="939.32" y="691" width="28.91" height="277.23" /><rect class="cls-4" x="1077.89" y="691" width="28.91" height="207.81" /><rect class="cls-4" x="748.81" y="894.15" width="51.93" height="40.42" /><path class="cls-4" d="M1061.47,988.45l-1.31.09-1.32.35a7.92,7.92,0,0,0,.61,15.11l1.32.26h1.41a7.94,7.94,0,0,0,1.93-15.37l-1.32-.35Z" transform="translate(-171.19 -54.67)" /><rect class="cls-4" x="881.59" y="932.99" width="17.31" height="17.31" /><rect class="cls-4" x="1008.65" y="691" width="28.82" height="190.5" /><rect class="cls-4" x="870.07" y="691" width="28.82" height="239.09" /><rect class="cls-4" x="837.48" y="930.09" width="17.4" height="106.85" /><rect class="cls-4" x="885.01" y="1039.05" width="48.68" height="23.11" /><rect class="cls-4" x="840.29" y="1044.76" width="5.45" height="20.83" /><line class="cls-4" x1="94.18" y1="233.28" x2="73.18" y2="233.28" /><line class="cls-4" x1="71.07" y1="233.28" x2="66.68" y2="233.28" /><line class="cls-4" x1="64.57" y1="233.28" x2="47.26" y2="233.28" /><line class="cls-4" x1="45.06" y1="233.28" x2="40.76" y2="233.28" /><line class="cls-4" x1="38.56" y1="233.28" x2="17.65" y2="233.28" /><line class="cls-4" x1="55.87" y1="194.97" x2="55.87" y2="215.97" /><line class="cls-4" x1="55.87" y1="218.17" x2="55.87" y2="222.47" /><line class="cls-4" x1="55.87" y1="224.67" x2="55.87" y2="241.98" /><line class="cls-4" x1="55.87" y1="244.09" x2="55.87" y2="248.48" /><line class="cls-4" x1="55.87" y1="250.59" x2="55.87" y2="271.59" /><rect class="cls-4" x="51.57" y="228.98" width="8.61" height="8.61" /><line class="cls-4" x1="51.74" y1="237.59" x2="51.57" y2="237.41" /><line class="cls-4" x1="52.62" y1="237.59" x2="51.57" y2="236.62" /><line class="cls-4" x1="53.41" y1="237.59" x2="51.57" y2="235.74" /><line class="cls-4" x1="54.2" y1="237.59" x2="51.57" y2="234.95" /><line class="cls-4" x1="54.99" y1="237.59" x2="51.57" y2="234.16" /><line class="cls-4" x1="55.87" y1="237.59" x2="51.57" y2="233.37" /><line class="cls-4" x1="56.66" y1="237.59" x2="51.57" y2="232.49" /><line class="cls-4" x1="57.45" y1="237.59" x2="51.57" y2="231.7" /><line class="cls-4" x1="58.33" y1="237.59" x2="51.57" y2="230.91" /><line class="cls-4" x1="59.12" y1="237.59" x2="51.57" y2="230.03" /><line class="cls-4" x1="59.91" y1="237.59" x2="51.57" y2="229.24" /><line class="cls-4" x1="60.18" y1="237.06" x2="52.09" y2="228.98" /><line class="cls-4" x1="60.18" y1="236.27" x2="52.88" y2="228.98" /><line class="cls-4" x1="60.18" y1="235.48" x2="53.67" y2="228.98" /><line class="cls-4" x1="60.18" y1="234.6" x2="54.55" y2="228.98" /><line class="cls-4" x1="60.18" y1="233.81" x2="55.34" y2="228.98" /><line class="cls-4" x1="60.18" y1="233.02" x2="56.13" y2="228.98" /><line class="cls-4" x1="60.18" y1="232.23" x2="57.01" y2="228.98" /><line class="cls-4" x1="60.18" y1="231.35" x2="57.8" y2="228.98" /><line class="cls-4" x1="60.18" y1="230.56" x2="58.6" y2="228.98" /><line class="cls-4" x1="60.18" y1="229.77" x2="59.47" y2="228.98" /><rect class="cls-4" x="306.65" y="706.64" width="86.64" height="86.64" /><rect class="cls-4" x="829.92" y="293.03" width="47.71" height="36.55" /><line class="cls-4" x1="1326.65" y1="98.49" x2="1326.65" y2="127.31" /><polyline class="cls-4" points="1263.12 81.18 1263.12 127.31 1326.65 127.31" /><polyline class="cls-4" points="1309.25 81.18 1309.25 98.49 1326.65 98.49" /><line class="cls-4" x1="1263.12" y1="81.18" x2="1309.25" y2="81.18" /><line class="cls-4" x1="1263.12" y1="99.02" x2="1268.83" y2="99.02" /><polyline class="cls-4" points="1263.12 118.08 1268.83 118.08 1268.83 99.02" /><polyline class="cls-4" points="1243.35 136.98 1243.35 239.43 1312.85 239.43" /><polyline class="cls-4" points="1243.35 136.98 1252.05 136.98 1252.05 159.73" /><line class="cls-4" x1="1312.85" y1="239.43" x2="1312.85" y2="159.73" /><line class="cls-4" x1="1249.15" y1="180.21" x2="1243.35" y2="180.21" /><polyline class="cls-4" points="1243.35 218.96 1249.15 218.96 1249.15 180.21" /><polyline class="cls-4" points="1278.06 239.43 1278.06 199.63 1312.85 199.63" /><line class="cls-4" x1="1312.85" y1="159.73" x2="1243.35" y2="159.73" /><line class="cls-4" x1="1312.85" y1="356.65" x2="1243.35" y2="356.65" /><line class="cls-4" x1="1312.85" y1="276.95" x2="1243.35" y2="276.95" /><line class="cls-4" x1="1252.05" y1="276.95" x2="1252.05" y2="254.19" /><line class="cls-4" x1="1312.85" y1="356.65" x2="1312.85" y2="276.95" /><line class="cls-4" x1="1249.15" y1="297.43" x2="1243.35" y2="297.43" /><polyline class="cls-4" points="1243.35 336.18 1249.15 336.18 1249.15 297.43" /><line class="cls-4" x1="1243.35" y1="254.19" x2="1252.05" y2="254.19" /><polyline class="cls-4" points="1278.06 356.65 1278.06 316.76 1312.85 316.76" /><line class="cls-4" x1="1243.35" y1="356.65" x2="1243.35" y2="254.19" /><polyline class="cls-4" points="1076.83 168.43 1179.29 168.43 1179.29 99.02" /><line class="cls-4" x1="1076.83" y1="159.73" x2="1076.83" y2="168.43" /><line class="cls-4" x1="1099.59" y1="99.02" x2="1099.59" y2="168.43" /><line class="cls-4" x1="1099.59" y1="159.73" x2="1076.83" y2="159.73" /><line class="cls-4" x1="1179.29" y1="99.02" x2="1099.59" y2="99.02" /><line class="cls-4" x1="1120.06" y1="162.63" x2="1120.06" y2="168.43" /><polyline class="cls-4" points="1158.82 168.43 1158.82 162.63 1120.07 162.63" /><polyline class="cls-4" points="1099.59 133.72 1139.48 133.72 1139.48 99.02" /><line class="cls-4" x1="990.19" y1="99.02" x2="990.19" y2="243.3" /><polyline class="cls-4" points="900.74 227.75 900.74 99.02 990.19 99.02" /><line class="cls-4" x1="990.19" y1="243.3" x2="974.9" y2="243.3" /><line class="cls-4" x1="990.19" y1="227.74" x2="900.74" y2="227.74" /><line class="cls-4" x1="974.9" y1="227.74" x2="974.9" y2="243.3" /><polyline class="cls-4" points="900.74 227.75 900.74 243.3 946.87 243.3 946.87 227.75" /><polyline class="cls-4" points="877.63 177.84 877.63 198.84 900.74 198.84" /><line class="cls-4" x1="900.74" y1="177.84" x2="877.63" y2="177.84" /><polyline class="cls-4" points="1019.1 134.51 1019.1 192.25 990.19 192.25" /><line class="cls-4" x1="990.19" y1="134.52" x2="1019.1" y2="134.52" /><polyline class="cls-4" points="1010.4 192.25 1010.4 157.63 990.19 157.63" /><rect class="cls-4" x="1296.16" y="570.17" width="49.47" height="14.41" /><rect class="cls-4" x="1335.61" y="590.91" width="10.02" height="19.6" /><rect class="cls-4" x="1226.21" y="570.17" width="49.47" height="14.41" /><rect class="cls-4" x="1062.69" y="576.5" width="17.31" height="17.31" /><rect class="cls-4" x="1114.97" y="887.21" width="23.02" height="11.6" /><rect class="cls-4" x="1040.37" y="875.7" width="11.6" height="23.11" /><rect class="cls-4" x="994.24" y="858.39" width="11.51" height="23.11" /><rect class="cls-4" x="1008.65" y="887.3" width="5.8" height="23.02" /><rect class="cls-4" x="870.07" y="932.99" width="9.4" height="17.31" /><rect class="cls-4" x="841.6" y="882.82" width="11.51" height="23.11" /><path class="cls-4" d="M1024.22,972.63l-.09-1.14-.35-1.06a5.73,5.73,0,0,0-10.9,1.06l-.09,1.14.09,1.14a5.73,5.73,0,0,0,10.9,1.06l.35-1.06Z" transform="translate(-171.19 -54.67)" /><rect class="cls-4" x="812.7" y="882.82" width="20.21" height="8.7" /><rect class="cls-4" x="800.75" y="691" width="28.91" height="138.04" /><rect class="cls-4" x="731.5" y="785.72" width="28.91" height="43.32" /><rect class="cls-4" x="743.01" y="733.79" width="17.4" height="28.91" /><rect class="cls-4" x="861.64" y="1047.75" width="14.41" height="14.41" /><rect class="cls-4" x="1076.39" y="954.08" width="8.7" height="28.91" /><rect class="cls-4" x="995.73" y="1044.85" width="43.32" height="17.31" /><rect class="cls-4" x="1056.45" y="1057.41" width="28.82" height="8.61" /><rect class="cls-4" x="1093.62" y="969.81" width="43.32" height="20.21" /><rect class="cls-4" x="1099.94" y="1045.29" width="20.21" height="20.21" /><rect class="cls-4" x="1133.42" y="1051.09" width="57.73" height="14.41" /><rect class="cls-4" x="1133.42" y="997.57" width="51.93" height="43.32" /><rect class="cls-4" x="1100.03" y="912.52" width="72.14" height="11.51" /><rect class="cls-4" x="1093.62" y="955.13" width="78.56" height="11.51" /><rect class="cls-4" x="1179.55" y="912.52" width="11.6" height="42.62" /><path class="cls-4" d="M1513.83,674.66l-.09-1.14-.35-1a5.73,5.73,0,0,0-10.9,1l-.08,1.14.08,1.15a5.73,5.73,0,0,0,10.9,1.05l.35-1.05Z" transform="translate(-171.19 -54.67)" /><rect class="cls-4" x="1322.52" y="630.01" width="23.11" height="57.73" /><rect class="cls-4" x="565.87" y="900.83" width="28.91" height="144.37" /><rect class="cls-4" x="534.15" y="900.83" width="28.91" height="144.37" /><rect class="cls-4" x="326.51" y="793.28" width="17.31" height="52.98" /><rect class="cls-4" x="606.29" y="1045.55" width="72.23" height="20.21" /><rect class="cls-4" x="688.18" y="1051.35" width="72.23" height="14.41" /><rect class="cls-4" x="638.1" y="869.46" width="86.55" height="28.91" /><rect class="cls-4" x="309.55" y="684.67" width="34.62" height="17.4" /><rect class="cls-4" x="349.97" y="684.67" width="34.62" height="17.4" /><rect class="cls-4" x="494.25" y="530.02" width="115.46" height="28.91" /><rect class="cls-4" x="630.45" y="530.02" width="129.96" height="28.91" /><rect class="cls-4" x="776.76" y="530.02" width="63.44" height="23.11" /><rect class="cls-4" x="846" y="548.29" width="23.11" height="17.31" /><rect class="cls-4" x="1099.86" y="548.29" width="159.04" height="17.4" /><rect class="cls-4" x="1241.59" y="506.56" width="17.31" height="17.31" /><rect class="cls-4" x="1088.87" y="506.56" width="34.62" height="17.31" /><rect class="cls-4" x="545.13" y="498.3" width="23.11" height="23.11" /><rect class="cls-4" x="1027.54" y="70.11" width="57.73" height="17.31" /><rect class="cls-4" x="1204.33" y="70.11" width="28.91" height="17.31" /><rect class="cls-4" x="843.01" y="209.91" width="17.31" height="57.73" /><rect class="cls-4" x="580.63" y="70.11" width="37.96" height="17.31" /><rect class="cls-4" x="306.65" y="185.66" width="17.31" height="57.73" /><rect class="cls-4" x="306.65" y="87.51" width="17.31" height="57.73" /><rect class="cls-4" x="328.53" y="548.38" width="57.82" height="17.31" /><rect class="cls-4" x="368.95" y="506.03" width="17.4" height="42.35" /><rect class="cls-4" x="306.3" y="506.03" width="17.31" height="52.9" /><rect class="cls-4" x="306.3" y="485.82" width="20.21" height="20.21" /><rect class="cls-4" x="368.95" y="422.64" width="17.4" height="46.22" /><rect class="cls-4" x="319.13" y="431.34" width="28.91" height="37.52" /><rect class="cls-4" x="250.06" y="681.86" width="28.82" height="11.51" /><rect class="cls-4" x="146.82" y="1036.59" width="86.64" height="28.82" /><rect class="cls-4" x="101.92" y="1053.9" width="44.9" height="11.51" /><rect class="cls-4" x="46.73" y="1027.98" width="16.26" height="20.21" /><rect class="cls-4" x="46.73" y="746.88" width="8.7" height="28.82" /><rect class="cls-4" x="58.24" y="630.8" width="72.23" height="17.31" /><rect class="cls-4" x="46.73" y="700.66" width="20.21" height="28.91" /><rect class="cls-4" x="46.73" y="660.24" width="20.21" height="28.91" /><rect class="cls-4" x="287.59" y="78.98" width="14.41" height="14.41" /><rect class="cls-4" x="287.59" y="102.09" width="14.41" height="14.41" /><rect class="cls-4" x="273.17" y="128.72" width="28.82" height="187.6" /><rect class="cls-4" x="46.73" y="87.42" width="23.11" height="46.13" /><rect class="cls-4" x="46.73" y="139.35" width="28.91" height="80.84" /><rect class="cls-4" x="46.73" y="403.4" width="28.91" height="80.84" /><rect class="cls-4" x="52.53" y="515.43" width="23.11" height="28.82" /><rect class="cls-4" x="670.87" y="326.78" width="57.73" height="8.61" /><rect class="cls-4" x="589.51" y="297.87" width="28.91" height="33.22" /><line class="cls-4" x1="370.89" y1="386.35" x2="391.8" y2="386.35" /><line class="cls-4" x1="393.99" y1="386.35" x2="398.3" y2="386.35" /><line class="cls-4" x1="400.5" y1="386.35" x2="417.81" y2="386.35" /><line class="cls-4" x1="420" y1="386.35" x2="424.31" y2="386.35" /><line class="cls-4" x1="426.51" y1="386.35" x2="447.42" y2="386.35" /><line class="cls-4" x1="409.11" y1="424.57" x2="409.11" y2="403.66" /><line class="cls-4" x1="409.11" y1="401.46" x2="409.11" y2="397.16" /><line class="cls-4" x1="409.11" y1="394.96" x2="409.11" y2="377.65" /><line class="cls-4" x1="409.11" y1="375.46" x2="409.11" y2="371.15" /><line class="cls-4" x1="409.11" y1="368.95" x2="409.11" y2="348.04" /><rect class="cls-4" x="404.8" y="381.96" width="8.7" height="8.7" /><line class="cls-4" x1="413.24" y1="381.96" x2="413.5" y2="382.22" /><line class="cls-4" x1="412.45" y1="381.96" x2="413.5" y2="383.01" /><line class="cls-4" x1="411.66" y1="381.96" x2="413.5" y2="383.8" /><line class="cls-4" x1="410.78" y1="381.96" x2="413.5" y2="384.59" /><line class="cls-4" x1="409.99" y1="381.96" x2="413.5" y2="385.47" /><line class="cls-4" x1="409.2" y1="381.96" x2="413.5" y2="386.26" /><line class="cls-4" x1="408.41" y1="381.96" x2="413.5" y2="387.05" /><line class="cls-4" x1="407.53" y1="381.96" x2="413.5" y2="387.93" /><line class="cls-4" x1="406.74" y1="381.96" x2="413.5" y2="388.72" /><line class="cls-4" x1="405.95" y1="381.96" x2="413.5" y2="389.51" /><line class="cls-4" x1="405.07" y1="381.96" x2="413.5" y2="390.39" /><line class="cls-4" x1="404.8" y1="382.48" x2="412.98" y2="390.66" /><line class="cls-4" x1="404.8" y1="383.36" x2="412.1" y2="390.66" /><line class="cls-4" x1="404.8" y1="384.15" x2="411.31" y2="390.66" /><line class="cls-4" x1="404.8" y1="384.95" x2="410.51" y2="390.66" /><line class="cls-4" x1="404.8" y1="385.74" x2="409.72" y2="390.66" /><line class="cls-4" x1="404.8" y1="386.61" x2="408.85" y2="390.66" /><line class="cls-4" x1="404.8" y1="387.4" x2="408.05" y2="390.66" /><line class="cls-4" x1="404.8" y1="388.2" x2="407.26" y2="390.66" /><line class="cls-4" x1="404.8" y1="389.08" x2="406.38" y2="390.66" /><line class="cls-4" x1="404.8" y1="389.87" x2="405.59" y2="390.66" /><line class="cls-4" x1="1288.95" y1="143.57" x2="1309.95" y2="143.57" /><line class="cls-4" x1="1312.06" y1="143.57" x2="1316.46" y2="143.57" /><line class="cls-4" x1="1318.56" y1="143.57" x2="1335.87" y2="143.57" /><line class="cls-4" x1="1338.07" y1="143.57" x2="1342.38" y2="143.57" /><line class="cls-4" x1="1344.57" y1="143.57" x2="1365.49" y2="143.57" /><line class="cls-4" x1="1327.26" y1="181.79" x2="1327.26" y2="160.88" /><line class="cls-4" x1="1327.26" y1="158.68" x2="1327.26" y2="154.37" /><line class="cls-4" x1="1327.26" y1="152.18" x2="1327.26" y2="134.87" /><line class="cls-4" x1="1327.26" y1="132.76" x2="1327.26" y2="128.36" /><line class="cls-4" x1="1327.26" y1="126.26" x2="1327.26" y2="105.26" /><rect class="cls-4" x="1322.87" y="139.17" width="8.7" height="8.7" /><line class="cls-4" x1="1331.39" y1="139.17" x2="1331.57" y2="139.44" /><line class="cls-4" x1="1330.51" y1="139.17" x2="1331.57" y2="140.23" /><line class="cls-4" x1="1329.72" y1="139.17" x2="1331.57" y2="141.02" /><line class="cls-4" x1="1328.93" y1="139.17" x2="1331.57" y2="141.9" /><line class="cls-4" x1="1328.14" y1="139.17" x2="1331.57" y2="142.69" /><line class="cls-4" x1="1327.26" y1="139.17" x2="1331.57" y2="143.48" /><line class="cls-4" x1="1326.47" y1="139.17" x2="1331.57" y2="144.36" /><line class="cls-4" x1="1325.68" y1="139.17" x2="1331.57" y2="145.15" /><line class="cls-4" x1="1324.8" y1="139.17" x2="1331.57" y2="145.94" /><line class="cls-4" x1="1324.01" y1="139.17" x2="1331.57" y2="146.73" /><line class="cls-4" x1="1323.22" y1="139.17" x2="1331.57" y2="147.61" /><line class="cls-4" x1="1322.87" y1="139.7" x2="1331.04" y2="147.87" /><line class="cls-4" x1="1322.87" y1="140.58" x2="1330.25" y2="147.87" /><line class="cls-4" x1="1322.87" y1="141.37" x2="1329.37" y2="147.87" /><line class="cls-4" x1="1322.87" y1="142.16" x2="1328.58" y2="147.87" /><line class="cls-4" x1="1322.87" y1="143.04" x2="1327.79" y2="147.87" /><line class="cls-4" x1="1322.87" y1="143.83" x2="1327" y2="147.87" /><line class="cls-4" x1="1322.87" y1="144.62" x2="1326.12" y2="147.87" /><line class="cls-4" x1="1322.87" y1="145.41" x2="1325.33" y2="147.87" /><line class="cls-4" x1="1322.87" y1="146.29" x2="1324.54" y2="147.87" /><line class="cls-4" x1="1322.87" y1="147.08" x2="1323.66" y2="147.87" /><line class="cls-4" x1="279.76" y1="162.46" x2="300.68" y2="162.46" /><line class="cls-4" x1="302.87" y1="162.46" x2="307.18" y2="162.46" /><line class="cls-4" x1="309.38" y1="162.46" x2="326.69" y2="162.46" /><line class="cls-4" x1="328.88" y1="162.46" x2="333.19" y2="162.46" /><line class="cls-4" x1="335.39" y1="162.46" x2="356.3" y2="162.46" /><line class="cls-4" x1="317.99" y1="200.77" x2="317.99" y2="179.86" /><line class="cls-4" x1="317.99" y1="177.66" x2="317.99" y2="173.35" /><line class="cls-4" x1="317.99" y1="171.16" x2="317.99" y2="153.85" /><line class="cls-4" x1="317.99" y1="151.65" x2="317.99" y2="147.34" /><line class="cls-4" x1="317.99" y1="145.15" x2="317.99" y2="124.23" /><rect class="cls-4" x="313.68" y="158.15" width="8.7" height="8.7" /><line class="cls-4" x1="322.12" y1="158.15" x2="322.38" y2="158.33" /><line class="cls-4" x1="321.33" y1="158.15" x2="322.38" y2="159.21" /><line class="cls-4" x1="320.54" y1="158.15" x2="322.38" y2="160" /><line class="cls-4" x1="319.66" y1="158.15" x2="322.38" y2="160.79" /><line class="cls-4" x1="318.87" y1="158.15" x2="322.38" y2="161.67" /><line class="cls-4" x1="318.08" y1="158.15" x2="322.38" y2="162.46" /><line class="cls-4" x1="317.28" y1="158.15" x2="322.38" y2="163.25" /><line class="cls-4" x1="316.41" y1="158.15" x2="322.38" y2="164.13" /><line class="cls-4" x1="315.62" y1="158.15" x2="322.38" y2="164.92" /><line class="cls-4" x1="314.82" y1="158.15" x2="322.38" y2="165.71" /><line class="cls-4" x1="313.95" y1="158.15" x2="322.38" y2="166.5" /><line class="cls-4" x1="313.68" y1="158.68" x2="321.85" y2="166.85" /><line class="cls-4" x1="313.68" y1="159.47" x2="320.98" y2="166.85" /><line class="cls-4" x1="313.68" y1="160.35" x2="320.18" y2="166.85" /><line class="cls-4" x1="313.68" y1="161.14" x2="319.39" y2="166.85" /><line class="cls-4" x1="313.68" y1="161.93" x2="318.6" y2="166.85" /><line class="cls-4" x1="313.68" y1="162.81" x2="317.72" y2="166.85" /><line class="cls-4" x1="313.68" y1="163.6" x2="316.93" y2="166.85" /><line class="cls-4" x1="313.68" y1="164.39" x2="316.14" y2="166.85" /><line class="cls-4" x1="313.68" y1="165.18" x2="315.26" y2="166.85" /><line class="cls-4" x1="313.68" y1="166.06" x2="314.47" y2="166.85" /><line class="cls-4" x1="199.28" y1="845.03" x2="220.28" y2="845.03" /><line class="cls-4" x1="222.38" y1="845.03" x2="226.78" y2="845.03" /><line class="cls-4" x1="228.89" y1="845.03" x2="246.29" y2="845.03" /><line class="cls-4" x1="248.4" y1="845.03" x2="252.7" y2="845.03" /><line class="cls-4" x1="254.9" y1="845.03" x2="275.9" y2="845.03" /><line class="cls-4" x1="237.59" y1="883.26" x2="237.59" y2="862.34" /><line class="cls-4" x1="237.59" y1="860.15" x2="237.59" y2="855.84" /><line class="cls-4" x1="237.59" y1="853.64" x2="237.59" y2="836.33" /><line class="cls-4" x1="237.59" y1="834.22" x2="237.59" y2="829.83" /><line class="cls-4" x1="237.59" y1="827.72" x2="237.59" y2="806.72" /><rect class="cls-4" x="233.28" y="840.73" width="8.61" height="8.61" /><line class="cls-4" x1="241.72" y1="840.73" x2="241.89" y2="840.9" /><line class="cls-4" x1="240.93" y1="840.73" x2="241.89" y2="841.69" /><line class="cls-4" x1="240.05" y1="840.73" x2="241.89" y2="842.57" /><line class="cls-4" x1="239.26" y1="840.73" x2="241.89" y2="843.36" /><line class="cls-4" x1="238.47" y1="840.73" x2="241.89" y2="844.15" /><line class="cls-4" x1="237.59" y1="840.73" x2="241.89" y2="844.94" /><line class="cls-4" x1="236.8" y1="840.73" x2="241.89" y2="845.82" /><line class="cls-4" x1="236" y1="840.73" x2="241.89" y2="846.61" /><line class="cls-4" x1="235.21" y1="840.73" x2="241.89" y2="847.4" /><line class="cls-4" x1="234.34" y1="840.73" x2="241.89" y2="848.28" /><line class="cls-4" x1="233.54" y1="840.73" x2="241.89" y2="849.07" /><line class="cls-4" x1="233.28" y1="841.25" x2="241.37" y2="849.34" /><line class="cls-4" x1="233.28" y1="842.04" x2="240.57" y2="849.34" /><line class="cls-4" x1="233.28" y1="842.83" x2="239.78" y2="849.34" /><line class="cls-4" x1="233.28" y1="843.63" x2="238.9" y2="849.34" /><line class="cls-4" x1="233.28" y1="844.51" x2="238.11" y2="849.34" /><line class="cls-4" x1="233.28" y1="845.3" x2="237.32" y2="849.34" /><line class="cls-4" x1="233.28" y1="846.09" x2="236.44" y2="849.34" /><line class="cls-4" x1="233.28" y1="846.96" x2="235.65" y2="849.34" /><line class="cls-4" x1="233.28" y1="847.76" x2="234.86" y2="849.34" /><line class="cls-4" x1="233.28" y1="848.55" x2="234.07" y2="849.34" /><line class="cls-4" x1="794.68" y1="904.08" x2="815.6" y2="904.08" /><line class="cls-4" x1="817.79" y1="904.08" x2="822.1" y2="904.08" /><line class="cls-4" x1="824.29" y1="904.08" x2="841.6" y2="904.08" /><line class="cls-4" x1="843.71" y1="904.08" x2="848.11" y2="904.08" /><line class="cls-4" x1="850.22" y1="904.08" x2="871.22" y2="904.08" /><line class="cls-4" x1="832.91" y1="942.39" x2="832.91" y2="921.48" /><line class="cls-4" x1="832.91" y1="919.28" x2="832.91" y2="914.98" /><line class="cls-4" x1="832.91" y1="912.78" x2="832.91" y2="895.47" /><line class="cls-4" x1="832.91" y1="893.27" x2="832.91" y2="888.97" /><line class="cls-4" x1="832.91" y1="886.77" x2="832.91" y2="865.86" /><rect class="cls-4" x="828.6" y="899.77" width="8.61" height="8.7" /><line class="cls-4" x1="837.04" y1="899.77" x2="837.21" y2="899.95" /><line class="cls-4" x1="836.24" y1="899.77" x2="837.21" y2="900.83" /><line class="cls-4" x1="835.45" y1="899.77" x2="837.21" y2="901.62" /><line class="cls-4" x1="834.58" y1="899.77" x2="837.21" y2="902.41" /><line class="cls-4" x1="833.79" y1="899.77" x2="837.21" y2="903.29" /><line class="cls-4" x1="832.99" y1="899.77" x2="837.21" y2="904.08" /><line class="cls-4" x1="832.11" y1="899.77" x2="837.21" y2="904.87" /><line class="cls-4" x1="831.32" y1="899.77" x2="837.21" y2="905.75" /><line class="cls-4" x1="830.53" y1="899.77" x2="837.21" y2="906.54" /><line class="cls-4" x1="829.66" y1="899.77" x2="837.21" y2="907.33" /><line class="cls-4" x1="828.86" y1="899.77" x2="837.21" y2="908.12" /><line class="cls-4" x1="828.6" y1="900.3" x2="836.68" y2="908.47" /><line class="cls-4" x1="828.6" y1="901.09" x2="835.89" y2="908.47" /><line class="cls-4" x1="828.6" y1="901.97" x2="835.1" y2="908.47" /><line class="cls-4" x1="828.6" y1="902.76" x2="834.31" y2="908.47" /><line class="cls-4" x1="828.6" y1="903.55" x2="833.43" y2="908.47" /><line class="cls-4" x1="828.6" y1="904.43" x2="832.64" y2="908.47" /><line class="cls-4" x1="828.6" y1="905.22" x2="831.85" y2="908.47" /><line class="cls-4" x1="828.6" y1="906.01" x2="830.97" y2="908.47" /><line class="cls-4" x1="828.6" y1="906.8" x2="830.18" y2="908.47" /><line class="cls-4" x1="828.6" y1="907.68" x2="829.39" y2="908.47" /><line class="cls-4" x1="984.83" y1="900.13" x2="1005.75" y2="900.13" /><line class="cls-4" x1="1007.94" y1="900.13" x2="1012.25" y2="900.13" /><line class="cls-4" x1="1014.45" y1="900.13" x2="1031.76" y2="900.13" /><line class="cls-4" x1="1033.87" y1="900.13" x2="1038.26" y2="900.13" /><line class="cls-4" x1="1040.37" y1="900.13" x2="1061.37" y2="900.13" /><line class="cls-4" x1="1023.06" y1="938.44" x2="1023.06" y2="917.44" /><line class="cls-4" x1="1023.06" y1="915.33" x2="1023.06" y2="910.93" /><line class="cls-4" x1="1023.06" y1="908.83" x2="1023.06" y2="891.51" /><line class="cls-4" x1="1023.06" y1="889.32" x2="1023.06" y2="885.01" /><line class="cls-4" x1="1023.06" y1="882.82" x2="1023.06" y2="861.82" /><rect class="cls-4" x="1018.75" y="895.82" width="8.61" height="8.61" /><line class="cls-4" x1="1027.19" y1="895.82" x2="1027.36" y2="896" /><line class="cls-4" x1="1026.4" y1="895.82" x2="1027.36" y2="896.79" /><line class="cls-4" x1="1025.61" y1="895.82" x2="1027.36" y2="897.67" /><line class="cls-4" x1="1024.73" y1="895.82" x2="1027.36" y2="898.46" /><line class="cls-4" x1="1023.94" y1="895.82" x2="1027.36" y2="899.25" /><line class="cls-4" x1="1023.14" y1="895.82" x2="1027.36" y2="900.13" /><line class="cls-4" x1="1022.27" y1="895.82" x2="1027.36" y2="900.92" /><line class="cls-4" x1="1021.48" y1="895.82" x2="1027.36" y2="901.71" /><line class="cls-4" x1="1020.68" y1="895.82" x2="1027.36" y2="902.5" /><line class="cls-4" x1="1019.81" y1="895.82" x2="1027.36" y2="903.38" /><line class="cls-4" x1="1019.01" y1="895.82" x2="1027.36" y2="904.17" /><line class="cls-4" x1="1018.75" y1="896.35" x2="1026.84" y2="904.43" /><line class="cls-4" x1="1018.75" y1="897.14" x2="1026.04" y2="904.43" /><line class="cls-4" x1="1018.75" y1="897.93" x2="1025.25" y2="904.43" /><line class="cls-4" x1="1018.75" y1="898.81" x2="1024.46" y2="904.43" /><line class="cls-4" x1="1018.75" y1="899.6" x2="1023.58" y2="904.43" /><line class="cls-4" x1="1018.75" y1="900.39" x2="1022.79" y2="904.43" /><line class="cls-4" x1="1018.75" y1="901.27" x2="1022" y2="904.43" /><line class="cls-4" x1="1018.75" y1="902.06" x2="1021.12" y2="904.43" /><line class="cls-4" x1="1018.75" y1="902.85" x2="1020.33" y2="904.43" /><line class="cls-4" x1="1018.75" y1="903.64" x2="1019.54" y2="904.43" /><line class="cls-4" x1="970.07" y1="104.73" x2="991.07" y2="104.73" /><line class="cls-4" x1="993.27" y1="104.73" x2="997.57" y2="104.73" /><line class="cls-4" x1="999.68" y1="104.73" x2="1017.08" y2="104.73" /><line class="cls-4" x1="1019.19" y1="104.73" x2="1023.5" y2="104.73" /><line class="cls-4" x1="1025.69" y1="104.73" x2="1046.69" y2="104.73" /><line class="cls-4" x1="1008.38" y1="143.04" x2="1008.38" y2="122.04" /><line class="cls-4" x1="1008.38" y1="119.93" x2="1008.38" y2="115.62" /><line class="cls-4" x1="1008.38" y1="113.43" x2="1008.38" y2="96.12" /><line class="cls-4" x1="1008.38" y1="93.92" x2="1008.38" y2="89.61" /><line class="cls-4" x1="1008.38" y1="87.42" x2="1008.38" y2="66.5" /><rect class="cls-4" x="1004.08" y="100.42" width="8.61" height="8.7" /><line class="cls-4" x1="1012.51" y1="100.42" x2="1012.69" y2="100.6" /><line class="cls-4" x1="1011.72" y1="100.42" x2="1012.69" y2="101.48" /><line class="cls-4" x1="1010.84" y1="100.42" x2="1012.69" y2="102.27" /><line class="cls-4" x1="1010.05" y1="100.42" x2="1012.69" y2="103.06" /><line class="cls-4" x1="1009.26" y1="100.42" x2="1012.69" y2="103.94" /><line class="cls-4" x1="1008.38" y1="100.42" x2="1012.69" y2="104.73" /><line class="cls-4" x1="1007.59" y1="100.42" x2="1012.69" y2="105.52" /><line class="cls-4" x1="1006.8" y1="100.42" x2="1012.69" y2="106.31" /><line class="cls-4" x1="1006.01" y1="100.42" x2="1012.69" y2="107.19" /><line class="cls-4" x1="1005.13" y1="100.42" x2="1012.69" y2="107.98" /><line class="cls-4" x1="1004.34" y1="100.42" x2="1012.69" y2="108.77" /><line class="cls-4" x1="1004.08" y1="100.95" x2="1012.16" y2="109.12" /><line class="cls-4" x1="1004.08" y1="101.74" x2="1011.37" y2="109.12" /><line class="cls-4" x1="1004.08" y1="102.62" x2="1010.58" y2="109.12" /><line class="cls-4" x1="1004.08" y1="103.41" x2="1009.7" y2="109.12" /><line class="cls-4" x1="1004.08" y1="104.2" x2="1008.91" y2="109.12" /><line class="cls-4" x1="1004.08" y1="105.08" x2="1008.12" y2="109.12" /><line class="cls-4" x1="1004.08" y1="105.87" x2="1007.33" y2="109.12" /><line class="cls-4" x1="1004.08" y1="106.66" x2="1006.45" y2="109.12" /><line class="cls-4" x1="1004.08" y1="107.45" x2="1005.66" y2="109.12" /><line class="cls-4" x1="1004.08" y1="108.33" x2="1004.87" y2="109.12" /><line class="cls-4" x1="1183.95" y1="418.42" x2="1198.8" y2="403.57" /><line class="cls-4" x1="1200.29" y1="402.08" x2="1203.37" y2="399" /><line class="cls-4" x1="1204.86" y1="397.51" x2="1217.16" y2="385.21" /><line class="cls-4" x1="1218.65" y1="383.71" x2="1221.73" y2="380.64" /><line class="cls-4" x1="1223.23" y1="379.15" x2="1238.07" y2="364.29" /><line class="cls-4" x1="1238.07" y1="418.42" x2="1223.23" y2="403.57" /><line class="cls-4" x1="1221.73" y1="402.08" x2="1218.65" y2="399" /><line class="cls-4" x1="1217.16" y1="397.51" x2="1204.86" y2="385.21" /><line class="cls-4" x1="1203.37" y1="383.71" x2="1200.29" y2="380.64" /><line class="cls-4" x1="1198.8" y1="379.15" x2="1183.95" y2="364.29" /><rect class="cls-4" x="1377.85" y="441.68" width="8.7" height="8.7" transform="translate(-81.74 1053.33) rotate(-45)" /><line class="cls-4" x1="1210.84" y1="385.38" x2="1211.19" y2="385.38" /><line class="cls-4" x1="1210.31" y1="386" x2="1211.71" y2="386" /><line class="cls-4" x1="1209.69" y1="386.53" x2="1212.33" y2="386.53" /><line class="cls-4" x1="1209.17" y1="387.14" x2="1212.86" y2="387.14" /><line class="cls-4" x1="1208.55" y1="387.67" x2="1213.47" y2="387.67" /><line class="cls-4" x1="1207.93" y1="388.28" x2="1214" y2="388.28" /><line class="cls-4" x1="1207.41" y1="388.81" x2="1214.61" y2="388.81" /><line class="cls-4" x1="1206.79" y1="389.43" x2="1215.23" y2="389.43" /><line class="cls-4" x1="1206.27" y1="390.04" x2="1215.76" y2="390.04" /><line class="cls-4" x1="1205.65" y1="390.57" x2="1216.37" y2="390.57" /><line class="cls-4" x1="1205.12" y1="391.18" x2="1216.9" y2="391.18" /><line class="cls-4" x1="1205.21" y1="391.71" x2="1216.72" y2="391.71" /><line class="cls-4" x1="1205.83" y1="392.33" x2="1216.19" y2="392.33" /><line class="cls-4" x1="1206.44" y1="392.85" x2="1215.58" y2="392.85" /><line class="cls-4" x1="1206.97" y1="393.47" x2="1215.05" y2="393.47" /><line class="cls-4" x1="1207.58" y1="394.08" x2="1214.44" y2="394.08" /><line class="cls-4" x1="1208.11" y1="394.61" x2="1213.91" y2="394.61" /><line class="cls-4" x1="1208.73" y1="395.23" x2="1213.29" y2="395.23" /><line class="cls-4" x1="1209.25" y1="395.75" x2="1212.68" y2="395.75" /><line class="cls-4" x1="1209.87" y1="396.37" x2="1212.15" y2="396.37" /><line class="cls-4" x1="1210.48" y1="396.9" x2="1211.54" y2="396.9" /><rect class="cls-5" x="1208.91" y="120.64" width="29.4" height="34.8" /><rect class="cls-5" x="888.99" y="524.94" width="29.4" height="34.8" /><rect class="cls-5" x="506.3" y="484.72" width="29.4" height="34.8" /><rect class="cls-5" x="570.8" y="117.22" width="29.4" height="34.8" /></g></g>
                <path class="cls-6" d="M292.17,778.41" transform="translate(-171.19 -54.67)" />
                <g id="Rettifiche">
                    <text class="cls-7" transform="translate(112.76 664.12)">RETTIFICHE</text>
                    <text class="cls-8" transform="translate(106.76 716.1)">@ViewBag.Rettifiche %</text>
                    @*<polyline class="cls-9" points="159.92 720.67 144.28 736.31 135.5 727.53 126.7 736.33" />
                    <line class="cls-10" x1="159.92" y1="730.62" x2="159.92" y2="720.67" />
                    <line class="cls-10" x1="151.01" y1="720.67" x2="160.96" y2="720.67" />*@
                    <text class="cls-11" transform="translate(165.83 736.33)">
                        + <tspan class="cls-12" x="18.56" y="0">@ViewBag.RettificheComparison%</tspan>
                    </text>
                </g>
                <g id="Torni">
                    <text class="cls-13" transform="translate(503.63 188.5)">TORNI</text>
                    <text class="cls-8" transform="translate(491.44 243.86)">@ViewBag.Frese %</text>
                    @*<polyline class="cls-9" points="544.61 248.43 528.96 264.07 520.18 255.29 511.39 264.09" />
                    <line class="cls-10" x1="544.61" y1="258.38" x2="544.61" y2="248.43" />
                    <line class="cls-10" x1="535.69" y1="248.43" x2="545.64" y2="248.43" />*@
                    <text class="cls-11" transform="translate(550.51 264.09)">
                        + <tspan class="cls-12" x="18.56" y="0">@ViewBag.TorniComparison%</tspan>
                    </text>
                </g>
                <g id="Frese">
                    <text class="cls-14" transform="translate(676.27 398.29) scale(1.04 1)">
                        <tspan class="cls-15">F</tspan>
                        <tspan x="18.67" y="0">RESE</tspan>
                    </text>
                    <text class="cls-16" transform="translate(660.43 450.51) scale(1.04 1)">@ViewBag.Torni %</text>
                    @*<polyline class="cls-9" points="713.6 454.9 697.96 469.93 689.17 461.49 680.38 469.94" />
                    <line class="cls-10" x1="713.6" y1="464.46" x2="713.6" y2="454.9" />
                    <line class="cls-10" x1="704.69" y1="454.9" x2="714.64" y2="454.9" />*@
                    <text class="cls-17" transform="translate(719.51 469.94) scale(1.04 1)">
                        + <tspan class="cls-18" x="17.83" y="0">@ViewBag.FreseComparison%</tspan>
                    </text>
                </g>
                <g id="Montaggio">
                    <text class="cls-7" transform="translate(912.33 751.15)">
                        MON<tspan class="cls-19" x="50.46" y="0">T</tspan>
                        <tspan class="cls-20" x="61.78" y="0">A</tspan><tspan x="75.83" y="0">GGIO</tspan>
                    </text>
                    <text class="cls-8" transform="translate(912.58 803.13)">@ViewBag.Montaggio %</text>
                    @*<polyline class="cls-9" points="965.75 807.7 950.1 823.35 941.32 814.56 932.53 823.36" />
                    <line class="cls-10" x1="965.75" y1="817.65" x2="965.75" y2="807.7" />
                    <line class="cls-10" x1="956.83" y1="807.7" x2="966.78" y2="807.7" />*@
                    <text class="cls-11" transform="translate(971.65 823.36)">
                        + <tspan class="cls-12" x="18.56" y="0">@ViewBag.MontaggioComparison%</tspan>
                    </text>
                </g>
                <path class="cls-6" d="M299.19,331.59" transform="translate(-171.19 -54.67)" />
                <g id="Magazzino">
                    <text class="cls-7" transform="translate(116.75 217.55)">
                        M<tspan class="cls-20" x="17.52" y="0">A</tspan>
                        <tspan class="cls-21" x="31.57" y="0">G</tspan>
                        <tspan x="47.9" y="0">AZZINO</tspan>
                    </text>
                    <text class="cls-8" transform="translate(113.78 269.28)">@ViewBag.Magazzino %</text>
                    <text class="cls-22" transform="translate(172.85 289.5)">
                        + <tspan class="cls-12" x="18.56" y="0">@ViewBag.MagazzinoComparison%</tspan>
                    </text>
                    @*<polyline class="cls-23" points="164.27 290.46 148.63 274.81 139.85 283.59 131.05 274.8" />
                    <line class="cls-24" x1="164.27" y1="280.51" x2="164.27" y2="290.46" />
                    <line class="cls-24" x1="155.36" y1="290.46" x2="165.31" y2="290.46" />*@
                </g>
            </svg>
        </div>
    </div>
</div>


<div class="row d-none">
    <div class="col-md-4 ct">
        <canvas id="ChartMontaggio" width="300" height="300" style="max-width: 300px; max-height: 300px; "></canvas>
    </div>
    <div class="col-md-4 ct">
        <canvas id="ChartTorni" width="400" height="400"style="max-width: 300px; max-height: 300px; "></canvas>
    </div>
    <div class="col-md-4 ct">
        <canvas id="ChartFrese" width="400" height="400"style="max-width: 300px; max-height: 300px; "></canvas>
    </div>
    <div class="col-md-4 ct">
        <canvas id="ChartRettifiche" width="400" height="400"style="max-width: 300px; max-height: 300px; "></canvas>
    </div>
    <div class="col-md-4 ct">
        <canvas id="ChartMagazzino" width="400" height="400"style="max-width: 300px; max-height: 300px; "></canvas>
    </div>
</div>
<div class="row d-none">
    <div class="col-md-3 shadow container-graphs">
        <div class="Chart" style="position: relative; height:400px; width: 400px;">
            <p class="main-card-title">Magazzino</p>
            <canvas id="ChartMontaggio" width="400" height="400"></canvas>
            <div class="donut-inner" style="position: absolute; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center; bottom: 0; margin: 0 !important;">
                <div>
                    <h1 class="title_ov">89%</h1>
                    <h4>Periodo di produttività</h4>
                </div>

            </div>
        </div>
    </div>
    <div class="col-md-3 shadow container-graphs">
        <div class="Chart" style="position: relative; height:400px; width: 400px;">
            <p class="main-card-title">Torni</p>
            <canvas id="ChartTorni" width="400" height="400"></canvas>
            <div class="donut-inner" style="position: absolute; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center; bottom: 0; margin: 0 !important;">
                <div>
                    <h1 class="title_ov">89%</h1>
                    <h4>Periodo di produttività</h4>
                </div>

            </div>
        </div>
    </div>
    <div class="col-md-3 shadow container-graphs">
        <div class="Chart" style="position: relative; height:400px; width: 400px;">
            <p class="main-card-title">Frese</p>
            <canvas id="ChartFrese" width="400" height="400"></canvas>
            <div class="donut-inner" style="position: absolute; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center; bottom: 0; margin: 0 !important;">
                <div>
                    <h1 class="title_ov">89%</h1>
                    <h4>Periodo di produttività</h4>
                </div>

            </div>
        </div>
    </div>



</div>
<div class="row d-none">
    <div class="col-md-4 shadow container-graphs">
        <div class="Chart" style="position: relative; height:400px; width: 400px;">
            <p class="main-card-title">Rettifiche</p>
            <canvas id="ChartRettifiche" width="400" height="400"></canvas>
            <div class="donut-inner" style="position: absolute; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center; bottom: 0; margin: 0 !important;">
                <div>
                    <h1 class="title_ov">89%</h1>
                    <h4>Periodo di produttività</h4>
                </div>

            </div>
        </div>
    </div>
    <div class="col-md-4 shadow container-graphs">
        <div class="Chart" style="position: relative; height:400px; width: 400px;">
            <p class="main-card-title">Magazzino</p>
            <canvas id="ChartMagazzino" width="400" height="400"></canvas>
            <div class="donut-inner" style="position: absolute; width: 100%; height: 100%; display: flex; align-items: center; justify-content: center; bottom: 0; margin: 0 !important;">
                <div>
                    <h1 class="title_ov">89%</h1>
                    <h4>Periodo di produttività</h4>
                </div>

            </div>
        </div>
    </div>
</div>
<script>

    var ctxMontaggio = document.getElementById("ChartMontaggio");
    var ChartMontaggio = new Chart(ctxMontaggio, {
  type: 'doughnut',
        data: {
            labels: ["Montaggio"],
    datasets: [{
      label: '# di ore produttive',
        data: [@ViewBag.Montaggio,@Html.Raw(100 - ViewBag.Montaggio)],
      backgroundColor: [
        'rgba(236, 119, 38, 1)',
        'rgba(222, 222, 222, 0.5)',
      ],
      borderColor: [
        'rgba(236, 119, 38,1)',
        'rgba(222, 222, 222, 1)',
      ],
      borderWidth: 1
    }]
  },
    options: {
        cutout: '70%',
        responsive: true,
        plugin: [{
            id: 'my-doughnut-text-plugin',
            afterDraw: function (chart, option) {
                let theCenterText = "50%";
                const canvasBounds = canvas.getBoundingClientRect();
                const fontSz = Math.floor(canvasBounds.height * 0.10);
                chart.ctx.textBaseline = 'middle';
                chart.ctx.textAlign = 'center';
                chart.ctx.font = fontSz + 'px Arial';
                chart.ctx.fillText(theCenterText, canvasBounds.width / 2, canvasBounds.height * 0.70)
            }
        }]
  }
    });
    var ctxFrese = document.getElementById("ChartFrese");
    var ChartFrese = new Chart(ctxFrese, {
        type: 'doughnut',
        data: {
            labels: ["Frese"],
            datasets: [{
                label: '# di ore produttive',
                data: [@ViewBag.Frese,@Html.Raw(100 - ViewBag.Frese)],
                backgroundColor: [
                    'rgba(236, 119, 38, 1)',
                    'rgba(222, 222, 222, 0.5)',
                ],
                borderColor: [
                    'rgba(236, 119, 38,1)',
                    'rgba(222, 222, 222, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            cutout: '70%',
            responsive: true,
            plugin: [{
                id: 'my-doughnut-text-plugin',
                afterDraw: function (chart, option) {
                    let theCenterText = "50%";
                    const canvasBounds = canvas.getBoundingClientRect();
                    const fontSz = Math.floor(canvasBounds.height * 0.10);
                    chart.ctx.textBaseline = 'middle';
                    chart.ctx.textAlign = 'center';
                    chart.ctx.font = fontSz + 'px Arial';
                    chart.ctx.fillText(theCenterText, canvasBounds.width / 2, canvasBounds.height * 0.70)
                }
            }]
        }
    });
    var ctxTorni = document.getElementById("ChartTorni");
    var ChartTorni = new Chart(ctxTorni, {
        type: 'doughnut',
        data: {
            labels: ["Torni"],
            datasets: [{
                label: '# di ore produttive',
                data: [@ViewBag.Torni,@Html.Raw(100 - ViewBag.Torni)],
                backgroundColor: [
                    'rgba(236, 119, 38, 1)',
                    'rgba(222, 222, 222, 0.5)',
                ],
                borderColor: [
                    'rgba(236, 119, 38,1)',
                    'rgba(222, 222, 222, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            cutout: '70%',
            responsive: true,
            plugin: [{
                id: 'my-doughnut-text-plugin',
                afterDraw: function (chart, option) {
                    let theCenterText = "50%";
                    const canvasBounds = canvas.getBoundingClientRect();
                    const fontSz = Math.floor(canvasBounds.height * 0.10);
                    chart.ctx.textBaseline = 'middle';
                    chart.ctx.textAlign = 'center';
                    chart.ctx.font = fontSz + 'px Arial';
                    chart.ctx.fillText(theCenterText, canvasBounds.width / 2, canvasBounds.height * 0.70)
                }
            }]
        }
    });
    var ctxRettifiche = document.getElementById("ChartRettifiche");
    var ChartRettifiche = new Chart(ctxRettifiche, {
        type: 'doughnut',
        data: {
            labels: ["Rettifiche"],
            datasets: [{
                label: '# di ore produttive',
                data: [@ViewBag.Rettifiche,@Html.Raw(100 - ViewBag.Rettifiche)],
                backgroundColor: [
                    'rgba(236, 119, 38, 1)',
                    'rgba(222, 222, 222, 0.5)',
                ],
                borderColor: [
                    'rgba(236, 119, 38,1)',
                    'rgba(222, 222, 222, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            cutout: '70%',
            responsive: true,
            plugin: [{
                id: 'my-doughnut-text-plugin',
                afterDraw: function (chart, option) {
                    let theCenterText = "50%";
                    const canvasBounds = canvas.getBoundingClientRect();
                    const fontSz = Math.floor(canvasBounds.height * 0.10);
                    chart.ctx.textBaseline = 'middle';
                    chart.ctx.textAlign = 'center';
                    chart.ctx.font = fontSz + 'px Arial';
                    chart.ctx.fillText(theCenterText, canvasBounds.width / 2, canvasBounds.height * 0.70)
                }
            }]
        }
    });
    var ctxMagazzino = document.getElementById("ChartMagazzino");
    var ChartMagazzino = new Chart(ctxMagazzino, {
        type: 'doughnut',
        data: {
            labels: ["Magazzino"],
            datasets: [{
                label: '# di ore produttive',
                data: [@ViewBag.Magazzino,@Html.Raw(100 - ViewBag.Magazzino)],
                backgroundColor: [
                    'rgba(236, 119, 38, 1)',
                    'rgba(222, 222, 222, 0.5)',
                ],
                borderColor: [
                    'rgba(236, 119, 38,1)',
                    'rgba(222, 222, 222, 1)',
                ],
                borderWidth: 1
            }]
        },
        options: {
            cutout: '70%',
            responsive: true,
            plugin: [{
                id: 'my-doughnut-text-plugin',
                afterDraw: function (chart, option) {
                    let theCenterText = "50%";
                    const canvasBounds = canvas.getBoundingClientRect();
                    const fontSz = Math.floor(canvasBounds.height * 0.10);
                    chart.ctx.textBaseline = 'middle';
                    chart.ctx.textAlign = 'center';
                    chart.ctx.font = fontSz + 'px Arial';
                    chart.ctx.fillText(theCenterText, canvasBounds.width / 2, canvasBounds.height * 0.70)
                }
            }]
        }
    });
</script>