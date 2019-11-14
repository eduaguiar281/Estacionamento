var requestStart = false;
class Movimentacao {
    obterVeiculo() {
        if (requestStart)
            return;

        requestStart = true;
        let token = $('#frmVeiculo [name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;
        let placa = $("#placaVeiculo").val();
        let descricao = $("#descricao").val();
        let veiculo = { Id: 0, Placa: placa, Descricao: descricao };
        $('#btnSubmit').attr('disabled', true);
        $('#btnCancel').attr('disabled', true);
        $.ajax({
            url: "/Movimentacao/GetVeiculo",
            type: "POST",
            data: JSON.stringify(veiculo),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            headers: headers,
            success: function (result, status, request) {
                $('#placaVeiculoValidation').html("");
                let veiculo = JSON.parse(request.responseText);
                if (veiculo != null) {
                    $('#descricao').val(veiculo.descricao);
                    $('#veiculoId').val(veiculo.id);
                }
                requestStart = false;
            },
            error: function (request, status, erro) {
                $('#placaVeiculoValidation').html(request.responseText);
                requestStart = false;
            }
        });
    }

}

var movimentacao = new Movimentacao();

