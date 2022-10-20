@ModelType List(Of EuromaWeb.NotificheViewModel)
<div class="notifiche" style="z-index:10000;border-radius: 8px; height: 360px; width: 270px; position: absolute; bottom: 0; left: 0; box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px; background-color:white; top: 40px; left: 30px; overflow-y: scroll;">
    <div class="row" style=" width: 100%; padding: 0; margin: 0; border-radius:8px 8px 0px 0px; overflow:hidden;">
        <div class="col-md-12 bg-primary py-2" style="color: white; color: white; position: fixed; width: 270px; z-index: 1000; border-radius: 8px 8px 0px 0px;">
            NOTIFICHE
        </div>
    </div>
    <div style="height:40px;">

    </div>
    @If model.count = 0 Then
        @<div style="display: flex; align-content: center; justify-content:center; align-items: center; height:90%;">
        <div class="text-center">
            <h4>Evviva!🎉</h4>
            <p>Nessuna nuova notifica</p>
        </div>
        </div>
    End If
    @For Each m In Model
        @<a href="@m.Link" Class="NotificaLink">
            <div Class="row NotificaChild" style="min-height: 64px; width: 100%; padding: 0 !important; margin: 0">
                <div Class="col-md-3" style="padding: 0; display: flex; align-content: center; justify-content: center; align-items: center;">
                    <div style="width: 32px; height: 32px; border-radius: 150px; display: flex; align-content: center; justify-content: center; align-items: center;" Class="bg-primary">
                        <i Class="fa-solid @m.TipologiaNotifica" style="color:white;"></i>
                    </div>
                </div>
                <div Class="col-md-9" style="padding-left: 0 !important; display: flex; align-content: center; justify-content: center; align-items: center; font-size: 12px;">
                    <div class="row">
                        <div class="col-md-12">
                            @m.Descrizione
                            <p style="color: grey; font-size: 12px; margin:0!important;"><i class="fa-solid fa-clock"></i> @m.ElapsedTime</p>
                        </div>
                    </div>
                </div>
            </div>
            <hr style="width: 80%; margin-left: auto; margin-right: auto; margin-top: 0; margin-bottom: 0;" />
        </a>
    Next
</div>