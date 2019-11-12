class TabelaPrecos {
    confirmaExclusao() {
        let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;
        let id = $("#IdTabela").val();
        let tabelaId = { Id: id };
        $.ajax({
            url: "/TabelaPrecos/Delete",
            type: "POST",
            data: tabelaId,
            dataType: "json",
            headers: headers,
            success: function (result, status, request) {
                window.location.href = "/TabelaPrecos/Index";
            },
            error: function (request, status, erro) {
                if (request.status == "200") {
                    window.location.href = "/TabelaPrecos/Index";
                    return;
                }                
                var mensagem = "Não foi possível excluir tabela de preços! (";
                mensagem = mensagem.concat(request.responseText, ")");
                $("#alertMessage").html(mensagem);
                $("#alertError").fadeTo(7000, 1500).slideUp(1000, function () {
                    $("#alertError").slideUp(5000);
                });
                $("#exampleModalCenter").modal('hide');
            }
        });
    }

    excluir(id) {
        var mensagem = "Excluir Tabela de Preço - ";
        mensagem = mensagem.concat(id);
        $("#exampleModalLongTitle").html(mensagem);
        $("#exampleModalCenter").modal();
        $("#IdTabela").val(id);

    }
}

var tabelaPrecos = new TabelaPrecos();