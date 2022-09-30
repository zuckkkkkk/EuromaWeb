@ModelType EuromaWeb.OverviewOrdineViewModel
@Code
    ViewData("Title") = "Overview Ordine"
End Code
<style>
    .list-group-item.active {
        background: #ffc107;
    }
    /* end common class */
    .top-status ul {
        list-style: none;
        display: flex;
        justify-content: space-around;
        justify-content: center;
        flex-wrap: wrap;
        padding: 0;
        margin: 0;
    }

        .top-status ul li {
            width: 120px;
            height: 120px;
            border-radius: 50%;
            background: #fff;
            display: flex;
            justify-content: center;
            flex-direction: column;
            align-items: center;
            border: 8px solid #ddd;
            box-shadow: 1px 1px 10px 1px #ddd inset;
            margin: 10px 5px;
        }

            .top-status ul li.active {
                border-color: #ffc107;
                box-shadow: 1px 1px 20px 1px #ffc107 inset;
            }

            .top-status ul li.completed {
                border-color: #28a745;
                box-shadow: 1px 1px 20px 1px #28a745 inset;
            }
    /* end top status */

    ul.timeline {
        list-style-type: none;
        position: relative;
    }

        ul.timeline:before {
            content: ' ';
            background: #d4d9df;
            display: inline-block;
            position: absolute;
            left: 29px;
            width: 2px;
            height: 100%;
            z-index: 400;
        }

        ul.timeline > li {
            margin: 20px 0;
            padding-left: 30px;
        }

            ul.timeline > li:before {
                content: '\2713';
                background: #fff;
                display: inline-block;
                position: absolute;
                border-radius: 50%;
                border: 0;
                left: 5px;
                width: 50px;
                height: 50px;
                z-index: 400;
                text-align: center;
                line-height: 50px;
                color: #d4d9df;
                font-size: 24px;
                border: 2px solid #d4d9df;
            }

            ul.timeline > li.active:before {
                content: '\2713';
                background: #28a745;
                display: inline-block;
                position: absolute;
                border-radius: 50%;
                border: 0;
                left: 5px;
                width: 50px;
                height: 50px;
                z-index: 400;
                text-align: center;
                line-height: 50px;
                color: #fff;
                font-size: 30px;
                border: 2px solid #28a745;
            }
    /* end timeline */
</style>
<h2 style="margin-top: 1rem;">Panoramica Ordine </h2>
<h5>Ordine @Model.OC @IIf(Not IsNothing(ViewBag.OP), "(" + Html.Raw(ViewBag.OP).ToString + ")", "")</h5>
<div class="modal fade " id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                ...
            </div>
            <div class="modal-footer" style="border-top: none!important;">
                <Button type="button" class="btn btn-primary Add ModalSubmit">Aggiungi</Button>
                <Button type="button" id="Send_Btn" class="btn btn-primary Send ModalSubmit">Invia</Button>
                <Button type="button" class="btn btn-danger Delete ModalSubmit">Elimina</Button>
                <Button type="button" class="btn btn-primary Save ModalSubmit">Salva Modifiche</Button>
                <Button type="button" class="btn btn-secondary SaveClose ModalSubmit">Salva e Chiudi</Button>
            </div>
        </div>
    </div>
</div>
<section class="mb-5">
    <div class="container">
        <div class="main-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card" style="border:none!important;">
                        <div class="card-body">
                            <div class="top-status">
                                <ul>
                                    <li class="completed" id="FirstLvl">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="51.724" height="43.514" viewBox="0 0 51.724 43.514">
                                            <g id="Raggruppa_8" data-name="Raggruppa 8" transform="translate(-89.279 -415.488)">
                                                <path id="Icon_awesome-spell-check" data-name="Icon awesome-spell-check" d="M84.59,55.335,80.061,50.8a1.591,1.591,0,0,0-2.263,0L62.619,65.975l-5.552-5.561a1.591,1.591,0,0,0-2.263,0l-4.539,4.539a1.6,1.6,0,0,0,0,2.263L61.486,78.459a1.592,1.592,0,0,0,2.264,0L84.59,57.6a1.6,1.6,0,0,0,0-2.263Z" transform="translate(55.946 380.071)" />
                                                <path id="Tracciato_1" data-name="Tracciato 1" d="M18.426-22.447c5.742,0,9.147-3.641,9.147-10.219V-45.185H22.957v12.327c0,4.247-1.645,6-4.5,6-2.828,0-4.5-1.756-4.5-6V-45.185H9.279v12.518C9.279-26.088,12.684-22.447,18.426-22.447Zm21.469,0a9.963,9.963,0,0,0,8.339-3.992l-3-3.066a6.213,6.213,0,0,1-5.079,2.651c-3.78,0-6.464-2.938-6.464-7.153s2.684-7.153,6.464-7.153a6.266,6.266,0,0,1,5.079,2.619l3-3.066a10,10,0,0,0-8.31-3.96c-6.291,0-10.965,4.822-10.965,11.56S33.634-22.447,39.9-22.447Z" transform="translate(80 461.056)" />
                                            </g>
                                        </svg>
                                        <span>Completo</span>
                                    </li>
                                    @If Model.Stato_Generale > 2 Then
                                        @<li Class="completed" id="SecondLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="58.02" height="54.691" viewBox="0 0 58.02 54.691">
                                                <g id="Raggruppa_9" data-name="Raggruppa 9" transform="translate(-82 -396)">
                                                    <Text id="UT" transform="translate(82 425)" font-size="30" font-family="Montserrat-Bold, Montserrat" font-weight="700"><tspan x="0" y="0">UT</tspan></Text>
                                                    <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M26.227,7.294a13.387,13.387,0,1,0,0,18.933A13.392,13.392,0,0,0,26.227,7.294Zm-1.062,3.424a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,25.165,10.718ZM5.434,16.76a1.03,1.03,0,1,1,1.03,1.03A1.033,1.033,0,0,1,5.434,16.76ZM8.356,22.8a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,8.356,22.8Zm.38-10.676a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,8.735,12.127ZM11.1,6.953a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,11.1,6.953Zm-.5,7.124,1-1.712,6.126,3.662V25H15.731V17.056Zm1.911,12.118a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,12.506,26.195Zm4.254,1.892a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,28.087Zm0-20.593a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,7.494Zm5.663,19.074a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,22.423,26.568ZM22.8,8.356a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,22.8,8.356Zm3.765,14.068a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,26.568,22.423Zm.489-4.633a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,27.057,17.79Z" transform="translate(109.874 420.545)" />
                                                </g>
                                            </svg>
                                            <Span>Completo</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale < 2 Then
                                        @<li id="SecondLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="58.02" height="54.691" viewBox="0 0 58.02 54.691">
                                                <g id="Raggruppa_9" data-name="Raggruppa 9" transform="translate(-82 -396)">
                                                    <Text id="UT" transform="translate(82 425)" font-size="30" font-family="Montserrat-Bold, Montserrat" font-weight="700"><tspan x="0" y="0">UT</tspan></Text>
                                                    <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M26.227,7.294a13.387,13.387,0,1,0,0,18.933A13.392,13.392,0,0,0,26.227,7.294Zm-1.062,3.424a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,25.165,10.718ZM5.434,16.76a1.03,1.03,0,1,1,1.03,1.03A1.033,1.033,0,0,1,5.434,16.76ZM8.356,22.8a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,8.356,22.8Zm.38-10.676a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,8.735,12.127ZM11.1,6.953a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,11.1,6.953Zm-.5,7.124,1-1.712,6.126,3.662V25H15.731V17.056Zm1.911,12.118a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,12.506,26.195Zm4.254,1.892a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,28.087Zm0-20.593a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,7.494Zm5.663,19.074a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,22.423,26.568ZM22.8,8.356a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,22.8,8.356Zm3.765,14.068a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,26.568,22.423Zm.489-4.633a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,27.057,17.79Z" transform="translate(109.874 420.545)" />
                                                </g>
                                            </svg>
                                            <Span>In Attesa</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale = 2 Then
                                        @<li Class="active" id="SecondLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="58.02" height="54.691" viewBox="0 0 58.02 54.691">
                                                <g id="Raggruppa_9" data-name="Raggruppa 9" transform="translate(-82 -396)">
                                                    <Text id="UT" transform="translate(82 425)" font-size="30" font-family="Montserrat-Bold, Montserrat" font-weight="700"><tspan x="0" y="0">UT</tspan></Text>
                                                    <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M26.227,7.294a13.387,13.387,0,1,0,0,18.933A13.392,13.392,0,0,0,26.227,7.294Zm-1.062,3.424a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,25.165,10.718ZM5.434,16.76a1.03,1.03,0,1,1,1.03,1.03A1.033,1.033,0,0,1,5.434,16.76ZM8.356,22.8a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,8.356,22.8Zm.38-10.676a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,8.735,12.127ZM11.1,6.953a1.032,1.032,0,1,1-.38,1.409A1.033,1.033,0,0,1,11.1,6.953Zm-.5,7.124,1-1.712,6.126,3.662V25H15.731V17.056Zm1.911,12.118a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,12.506,26.195Zm4.254,1.892a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,28.087Zm0-20.593a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,16.76,7.494Zm5.663,19.074a1.032,1.032,0,1,1,.38-1.409A1.033,1.033,0,0,1,22.423,26.568ZM22.8,8.356a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,22.8,8.356Zm3.765,14.068a1.032,1.032,0,1,1-.38-1.409A1.037,1.037,0,0,1,26.568,22.423Zm.489-4.633a1.03,1.03,0,1,1,1.03-1.03A1.033,1.033,0,0,1,27.057,17.79Z" transform="translate(109.874 420.545)" />
                                                </g>
                                            </svg>
                                            <Span>In Lavoraz.</Span>
                                        </li>
                                    End If
                                    @If Model.Stato_Generale > 3 Then
                                        @<li Class="completed" id="ThirdLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="63.026" height="50.421" viewBox="0 0 63.026 50.421">
                                                <Path id="Icon_awesome-user-check" data-name="Icon awesome-user-check" d="M22.059,25.211A12.605,12.605,0,1,0,9.454,12.605,12.6,12.6,0,0,0,22.059,25.211Zm8.824,3.151H29.238a17.143,17.143,0,0,1-14.358,0H13.236A13.239,13.239,0,0,0,0,41.6v4.1a4.728,4.728,0,0,0,4.727,4.727H39.391a4.728,4.728,0,0,0,4.727-4.727V41.6A13.239,13.239,0,0,0,30.883,28.362ZM62.691,15.717,59.954,12.95a1.168,1.168,0,0,0-1.654-.01L47.979,23.182,43.5,18.672a1.168,1.168,0,0,0-1.654-.01l-2.767,2.748a1.168,1.168,0,0,0-.01,1.654l8.046,8.1a1.168,1.168,0,0,0,1.654.01L62.682,17.372a1.177,1.177,0,0,0,.01-1.654Z" />
                                            </svg>
                                            <Span>Completo</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale < 3 Then
                                        @<li id="ThirdLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="63.026" height="50.421" viewBox="0 0 63.026 50.421">
                                                <Path id="Icon_awesome-user-check" data-name="Icon awesome-user-check" d="M22.059,25.211A12.605,12.605,0,1,0,9.454,12.605,12.6,12.6,0,0,0,22.059,25.211Zm8.824,3.151H29.238a17.143,17.143,0,0,1-14.358,0H13.236A13.239,13.239,0,0,0,0,41.6v4.1a4.728,4.728,0,0,0,4.727,4.727H39.391a4.728,4.728,0,0,0,4.727-4.727V41.6A13.239,13.239,0,0,0,30.883,28.362ZM62.691,15.717,59.954,12.95a1.168,1.168,0,0,0-1.654-.01L47.979,23.182,43.5,18.672a1.168,1.168,0,0,0-1.654-.01l-2.767,2.748a1.168,1.168,0,0,0-.01,1.654l8.046,8.1a1.168,1.168,0,0,0,1.654.01L62.682,17.372a1.177,1.177,0,0,0,.01-1.654Z" />
                                            </svg>
                                            <Span>In Attesa</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale = 3 Then
                                        @<li Class="active" id="ThirdLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="63.026" height="50.421" viewBox="0 0 63.026 50.421">
                                                <Path id="Icon_awesome-user-check" data-name="Icon awesome-user-check" d="M22.059,25.211A12.605,12.605,0,1,0,9.454,12.605,12.6,12.6,0,0,0,22.059,25.211Zm8.824,3.151H29.238a17.143,17.143,0,0,1-14.358,0H13.236A13.239,13.239,0,0,0,0,41.6v4.1a4.728,4.728,0,0,0,4.727,4.727H39.391a4.728,4.728,0,0,0,4.727-4.727V41.6A13.239,13.239,0,0,0,30.883,28.362ZM62.691,15.717,59.954,12.95a1.168,1.168,0,0,0-1.654-.01L47.979,23.182,43.5,18.672a1.168,1.168,0,0,0-1.654-.01l-2.767,2.748a1.168,1.168,0,0,0-.01,1.654l8.046,8.1a1.168,1.168,0,0,0,1.654.01L62.682,17.372a1.177,1.177,0,0,0,.01-1.654Z" />
                                            </svg>
                                            <Span>In Lavoraz.</Span>
                                        </li>
                                    End If

                                    @If Model.Stato_Generale > 4 Then
                                        @<li Class="completed" id="FourthLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="51.724" height="51.719" viewBox="0 0 51.724 51.719">
                                                <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M47.525,10.946a25.864,25.864,0,1,0,0,36.577A25.873,25.873,0,0,0,47.525,10.946Zm-2.051,6.614a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,45.474,17.561ZM7.355,29.235a1.989,1.989,0,1,1,1.989,1.989A2,2,0,0,1,7.355,29.235ZM13,40.909a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,13,40.909Zm.734-20.626A1.994,1.994,0,1,1,13,17.561,2,2,0,0,1,13.733,20.283Zm4.563-10a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,18.3,10.288Zm-.97,13.763,1.939-3.307L31.1,27.817V45.148H27.248V29.807Zm3.692,23.411a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,21.019,47.461Zm8.218,3.655a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,51.116Zm0-39.784a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,11.332Zm10.941,36.85a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,40.177,48.182ZM40.911,13a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,40.911,13Zm7.273,27.178a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,48.184,40.175Zm.945-8.951a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,49.129,31.224Z" transform="translate(-3.372 -3.375)" />
                                            </svg>
                                            <Span>Completo</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale < 4 Then
                                        @<li id="FourthLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="51.724" height="51.719" viewBox="0 0 51.724 51.719">
                                                <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M47.525,10.946a25.864,25.864,0,1,0,0,36.577A25.873,25.873,0,0,0,47.525,10.946Zm-2.051,6.614a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,45.474,17.561ZM7.355,29.235a1.989,1.989,0,1,1,1.989,1.989A2,2,0,0,1,7.355,29.235ZM13,40.909a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,13,40.909Zm.734-20.626A1.994,1.994,0,1,1,13,17.561,2,2,0,0,1,13.733,20.283Zm4.563-10a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,18.3,10.288Zm-.97,13.763,1.939-3.307L31.1,27.817V45.148H27.248V29.807Zm3.692,23.411a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,21.019,47.461Zm8.218,3.655a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,51.116Zm0-39.784a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,11.332Zm10.941,36.85a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,40.177,48.182ZM40.911,13a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,40.911,13Zm7.273,27.178a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,48.184,40.175Zm.945-8.951a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,49.129,31.224Z" transform="translate(-3.372 -3.375)" />
                                            </svg>
                                            <Span>In Attesa</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale = 4 Then
                                        @<li Class="active" id="FourthLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="51.724" height="51.719" viewBox="0 0 51.724 51.719">
                                                <Path id="Icon_ionic-md-clock" data-name="Icon ionic-md-clock" d="M47.525,10.946a25.864,25.864,0,1,0,0,36.577A25.873,25.873,0,0,0,47.525,10.946Zm-2.051,6.614a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,45.474,17.561ZM7.355,29.235a1.989,1.989,0,1,1,1.989,1.989A2,2,0,0,1,7.355,29.235ZM13,40.909a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,13,40.909Zm.734-20.626A1.994,1.994,0,1,1,13,17.561,2,2,0,0,1,13.733,20.283Zm4.563-10a1.994,1.994,0,1,1-.734,2.723A2,2,0,0,1,18.3,10.288Zm-.97,13.763,1.939-3.307L31.1,27.817V45.148H27.248V29.807Zm3.692,23.411a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,21.019,47.461Zm8.218,3.655a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,51.116Zm0-39.784a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,29.237,11.332Zm10.941,36.85a1.994,1.994,0,1,1,.734-2.723A2,2,0,0,1,40.177,48.182ZM40.911,13a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,40.911,13Zm7.273,27.178a1.994,1.994,0,1,1-.734-2.723A2,2,0,0,1,48.184,40.175Zm.945-8.951a1.989,1.989,0,1,1,1.989-1.989A2,2,0,0,1,49.129,31.224Z" transform="translate(-3.372 -3.375)" />
                                            </svg>
                                            <Span>In Lavoraz.</Span>
                                        </li>
                                    End If
                                    @If Model.Stato_Generale >= 5 Then
                                        @<li Class="completed" id="FifthLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50.809" viewBox="0 0 50 50.809">
                                                <Path id="Icon_awesome-flag-checkered" data-name="Icon awesome-flag-checkered" d="M23.9,18.844V25.6c2.59.585,4.892,1.548,7.3,2.213V21.047c-2.58-.576-4.9-1.538-7.3-2.2ZM46.058,6.64C42.654,8.217,38.467,9.8,34.448,9.8c-5.309,0-9.7-3.453-16.392-3.453a19.292,19.292,0,0,0-6.747,1.191,5.557,5.557,0,1,0-8.365,2.57V48.424a2.376,2.376,0,0,0,2.381,2.381H6.913a2.376,2.376,0,0,0,2.381-2.381V39.057a27.665,27.665,0,0,1,11.351-2.193c5.319,0,9.7,3.453,16.392,3.453a20.734,20.734,0,0,0,12.155-4.058,3.166,3.166,0,0,0,1.369-2.62V9.517A3.171,3.171,0,0,0,46.058,6.64ZM16.6,32.3a31.26,31.26,0,0,0-7.3,1.647v-7a28.387,28.387,0,0,1,7.3-1.727ZM45.81,18.954a31.651,31.651,0,0,1-7.3,2.372V28.38a18.442,18.442,0,0,0,7.3-2.58v7a16.034,16.034,0,0,1-7.3,2.689v-7.1a16.784,16.784,0,0,1-7.3-.556v6.688A57.9,57.9,0,0,0,23.9,32.4V25.6a22.056,22.056,0,0,0-7.3-.377V18.279a35.016,35.016,0,0,0-7.3,2.074v-7c3.294-1.211,4.971-1.965,7.3-2.183v7.1a16.878,16.878,0,0,1,7.3.566V12.157a56.489,56.489,0,0,0,7.3,2.114v6.787a18.9,18.9,0,0,0,7.3.268V14.191a35.108,35.108,0,0,0,7.3-2.233Z" transform="translate(-0.563 0.003)" />
                                            </svg>
                                            <Span>Completo</Span>
                                        </li>
                                    ElseIf Model.Stato_Generale < 5 Then
                                        @<li id="FifthLvl">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50.809" viewBox="0 0 50 50.809">
                                                <Path id="Icon_awesome-flag-checkered" data-name="Icon awesome-flag-checkered" d="M23.9,18.844V25.6c2.59.585,4.892,1.548,7.3,2.213V21.047c-2.58-.576-4.9-1.538-7.3-2.2ZM46.058,6.64C42.654,8.217,38.467,9.8,34.448,9.8c-5.309,0-9.7-3.453-16.392-3.453a19.292,19.292,0,0,0-6.747,1.191,5.557,5.557,0,1,0-8.365,2.57V48.424a2.376,2.376,0,0,0,2.381,2.381H6.913a2.376,2.376,0,0,0,2.381-2.381V39.057a27.665,27.665,0,0,1,11.351-2.193c5.319,0,9.7,3.453,16.392,3.453a20.734,20.734,0,0,0,12.155-4.058,3.166,3.166,0,0,0,1.369-2.62V9.517A3.171,3.171,0,0,0,46.058,6.64ZM16.6,32.3a31.26,31.26,0,0,0-7.3,1.647v-7a28.387,28.387,0,0,1,7.3-1.727ZM45.81,18.954a31.651,31.651,0,0,1-7.3,2.372V28.38a18.442,18.442,0,0,0,7.3-2.58v7a16.034,16.034,0,0,1-7.3,2.689v-7.1a16.784,16.784,0,0,1-7.3-.556v6.688A57.9,57.9,0,0,0,23.9,32.4V25.6a22.056,22.056,0,0,0-7.3-.377V18.279a35.016,35.016,0,0,0-7.3,2.074v-7c3.294-1.211,4.971-1.965,7.3-2.183v7.1a16.878,16.878,0,0,1,7.3.566V12.157a56.489,56.489,0,0,0,7.3,2.114v6.787a18.9,18.9,0,0,0,7.3.268V14.191a35.108,35.108,0,0,0,7.3-2.233Z" transform="translate(-0.563 0.003)" />
                                            </svg>
                                            <Span>In Attesa</Span>
                                        </li>
                                    End If
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div Class="card mt-4" style="box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px; border:none!important;">
                        <div Class="card-body p-0 table-responsive">
                            <h4 Class="p-3 mb-0">Descrizione Ordine</h4>
                            <div class="p-3">
                                <Table Class="table mb-0 p-3" id="mainDataTable">
                                    <thead>
                                        <tr>
                                            <th scope="col"> Codice Articolo</th>
                                            <th scope="col"> Descrizione</th>
                                            <th scope="col"> Disinta Base</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        @For Each art In Model.ListArt
                                            @<tr>
                                                <td>
                                                    @art.Cod_Art
                                                </td>
                                                <td>@art.Descrizione</td>
                                                <td><strong>@IIf(art.DistintaBase, Html.Raw("<i class='fa-solid fa-check'></i>"), Html.Raw("<i class='fa-solid fa-xmark'></i>"))</strong></td>
                                            </tr>
                                        Next

                                    </tbody>
                                </Table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        @If Model.NoteList.Count > 0 Then
                            @<div Class="col-md-6">
                                <div Class="card mt-4 " style="overflow: hidden !important;box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px; border: none !important;">
                                    <div Class="card-body p-0 table-responsive">
                                        <h4 Class="p-3 mb-0">Lista Note</h4>
                                        <div Class="px-3">
                                            @For Each mess In Model.NoteList
                                                @<div Class="flex-shrink-1 bg-light rounded py-2 px-3 ml-3 mb-2 mt-1 ">
                                                    <div Class="font-weight-bold mb-1">@StrConv(mess.Operatore_Nome, VbStrConv.ProperCase), il @mess.Data_Nota.ToString.Split(" ")(0) alle @mess.Data_Nota.ToString.Split(" ")(1).Split(":")(0):@mess.Data_Nota.ToString.Split(" ")(1).Split(":")(1):</div>
                                                    @mess.Contenuto_Nota
                                                </div>
                                            Next
                                        </div>
                                    </div>
                                </div>
                            </div>
                        End If
                        @If Model.Documenti.Count > 0 Then
                            @<div Class="col-md-6">
                                <div Class="card mt-4 " style=" overflow: hidden !important; box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px; border: none !important;">
                                    <div Class="card-body">
                                        <h4> Lista Documenti </h4>
                                        <Table Class="table table-striped">
                                            <thead>
                                                <tr>
                                                    <td> Nome File</td>
                                                    <td> Proprietario</td>
                                                    <td> Data Caricamento</td>
                                                    <td> </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                @For Each file In Model.Documenti
                                                    @<tr>
                                                        <td>
                                                            @file.Nome_File
                                                        </td>
                                                        <td>
                                                            @file.Operatore_Nome
                                                        </td>
                                                        <td>
                                                            @file.DataCreazioneFile
                                                        </td>
                                                        <td>
                                                            <a href="@Url.Action("DownloadFile", "AccettazioneUC", New With {.id = file.Id})"> <i class="fa-solid fa-download"></i></a>
                                                        </td>
                                                    </tr>
                                                Next
                                            </tbody>
                                        </Table>

                                    </div>
                                </div>
                            </div>
                        End If
                    </div>
                    @If Model.ListaOP.Count > 0 Then
                        @<div Class="card mt-4" style="box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px; border:none!important;">
                            <div Class="card-body">
                                <h4> Ordini di Produzione</h4>
                                <div Class="accordion" id="accordionExample">

                                    <table class="table table-striped" id="mainDataTable">
                                        <thead>
                                            <tr>
                                                <td>
                                                    Ordine di Prod
                                                </td>
                                                <td>
                                                    Data Inizio attività
                                                </td>
                                                <td>
                                                    Concluso
                                                </td>
                                                <td>
                                                    Azioni
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            @For Each op In Model.ListaOP
                                                @<tr>
                                                    <td>
                                                        @op.OP (@op.Articolo)
                                                    </td>
                                                    <td>
                                                        ----
                                                    </td>
                                                    <td>
                                                        ----
                                                    </td>
                                                    <td>
                                                        @If User.IsInRole("Produzione") Or User.IsInRole("Admin") Then
                                                            @<button type="button" data-type="show_mancanti" data-value="@op.Articolo" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                                                <i class="fa-solid fa-file-pen"></i>
                                                            </button>
                                                            @*@<button type="button" data-type="show_gantt" data-value="@op.OP" Class="btn btn-primary w-auto" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                                                <i class="fa-solid fa-chart-gantt"></i>
                                                            </button>*@
                                                        End If
                                                        @If (User.IsInRole("Admin") Or User.IsInRole("Tecnico") Or User.IsInRole("TecnicoAdmin")) And op.PresenteInDbEsterno = False Then
                                                            @<button type="button" onclick="CreateODP('@op.OP')" Class="btn btn-primary w-auto">
                                                                <i class="fa-solid fa-plus"></i>
                                                            </button>
                                                        End If
                                                    </td>
                                                </tr>
                                            Next
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    End If
                    <div Class="card mt-4" style="box-shadow: rgba(50, 50, 93, 0.25) 0px 13px 27px -5px, rgba(0, 0, 0, 0.3) 0px 8px 16px -8px; border:none!important;">
                        <div Class="card-body">
                            <h4> Timeline</h4>
                            <ul Class="timeline">
                                @For Each s In Model.Timeline
                                    @<li Class="active">
                                        <h6> @s.Titolo</h6>
                                        <p Class="mb-0 text-muted">@s.Descrizione</p>
                                        <p Class="text-muted">@s.UltimaModifica.Data - @s.UltimaModifica.Operatore - @s.Ufficio</p>
                                    </li>
                                Next

                            </ul>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</section>
