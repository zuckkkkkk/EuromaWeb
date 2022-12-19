@ModelType List(Of ComputerViewModel)
@Code
    ViewData("Title") = "Lista PC"
End Code
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.0/css/all.min.css" integrity="sha512-xh6O/CkQoPOWDdYTDqeRdPCVd1SpvCA9XXcUnZS2FmJNp1coAFzvtCN9BmamE+4aHK8yyUHUSCcJHgXloTyT2A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
<style>
    @@-webkit-keyframes greenAnimation {
        0% {
            color: green;
        }

        50% {
            color: white;
        }

        100% {
            color: green;
        }
    }

    @@-webkit-keyframes redAnimation {
        0% {
            color: red;
        }

        50% {
            color: white;
        }

        100% {
            color: red;
        }
    }

    #Card_Menu {
        border: none !important;
        box-shadow: rgba(0, 0, 0, 0.18) 0px 2px 4px;
        min-height: 150px;
        display: flex;
        justify-content: center;
        align-items: center;
        transition: .2s ease-in-out;
    }

        #Card_Menu:hover {
            box-shadow: rgba(0, 0, 0, 0.15) 0px 15px 25px, rgba(0, 0, 0, 0.05) 0px 5px 10px;
            transition: .2s ease-in-out;
            cursor: pointer;
        }

    #Arrow {
        position: absolute;
        right: 16px;
        bottom: 8px;
    }
    #ButtonPC {
        position: absolute;
        right: 16px;
        top: 8px;
    }
    #Nome_Menu {
        font-weight: 700;
        font-size: 18px;
    }

    #Stato_Menu {
        font-weight: 500;
        font-size: 13px;
        position: absolute;
        top: 8px;
        left: 16px;
    }

    #Parent_Card_Menu {
        margin: 12px 0px;
    }

    .red {
        color: red;
        -webkit-animation-name: redAnimation;
        -webkit-animation-iteration-count: infinite;
        -webkit-animation-duration: 2s;
    }

    .green {
        color: green;
        -webkit-animation-name: greenAnimation;
        -webkit-animation-iteration-count: infinite;
        -webkit-animation-duration: 2s;
    }

    #Parent_Card_Menu a {
        text-decoration: none !important;
        color: black;
    }

    .modal-content {
        border-radius: 14px;
        border: none !Important;
    }

    .modal-header {
        border: none;
    }
</style>

<div class="modal fade " id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" style="opacity:1!important;">
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
<div class="container mt-5">
    <h5>Lista dei PC</h5>
    <div class="row">
        @For Each m In Model
            @<div Class="col-md-4" id="Parent_Card_Menu">
                 <div Class="card" id="Card_Menu">
                     @if m.Attivo Then
                         @<p id="Stato_Menu"><i class="fa-solid fa-circle green"></i>Acceso</p>
                         @<button class="btn btn-primary" id="ButtonPC" onclick="turnoff(@m.Id)">Spegni</button>
                     Else
                         @<button class="btn btn-primary" id="ButtonPC"onclick="turnon(@m.Id)">Accendi</button>
                         @<p id="Stato_Menu"><i class="fa-solid fa-circle red"></i>Spento</p>
                     End If
                     <p id="Nome_Menu">@m.NomePC</p>
                     <i id="Arrow" Class="fa-solid fa-arrow-right-long fa-2x"></i>
                     @*<a href="@Url.Action("EditMenu", "Home", New With {.id = m.Id})" class="stretched-link"></a>*@
                 </div>
            </div>
        Next
        <div Class="col-md-4" id="Parent_Card_Menu">
            <a data-type="add_computer" id="Add_Computer" Class="" data-bs-toggle="modal" data-bs-target="#exampleModal" class="stretched-link">
                <div Class="card" id="Card_Menu">
                    <i class="fas fa-plus-circle fa-3x"></i>
                </div>
            </a>
        </div>
    </div>
</div>

