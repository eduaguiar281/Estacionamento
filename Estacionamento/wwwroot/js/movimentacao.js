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
        if (placa == "" || placa == null) {
            requestStart = false;
            return;
        }
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
                $('#btnSubmit').attr('disabled', false);
                $('#btnCancel').attr('disabled', false);
            },
            error: function (request, status, erro) {
                $('#placaVeiculoValidation').html(request.responseText);
                requestStart = false;
                $('#btnSubmit').attr('disabled', false);
                $('#btnCancel').attr('disabled', false);
            }
        });
    }

    confirmaExclusao() {
        let token = $('[name=__RequestVerificationToken]').val();
        let headers = {};
        headers['RequestVerificationToken'] = token;
        let id = $("#IdMovimentacao").val();
        let tabelaId = { Id: id };
        $.ajax({
            url: "/Movimentacao/Delete",
            type: "POST",
            data: tabelaId,
            dataType: "json",
            headers: headers,
            success: function (result, status, request) {
                window.location.href = "/Movimentacao/Index";
            },
            error: function (request, status, erro) {
                if (request.status == "200") {
                    window.location.href = "/Movimentacao/Index";
                    return;
                }
                var mensagem = "Não foi possível excluir movimentacão! (";
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
        var mensagem = "Excluir Movimentação ID - ";
        mensagem = mensagem.concat(id);
        $("#exampleModalLongTitle").html(mensagem);
        $("#exampleModalCenter").modal();
        $("#IdMovimentacao").val(id);

    }

    calculaHora() {
        let id = $("#idMovimentacao").val();
        let dataHoraSaida = $("#dataHoraSaida").val();
        var urlString = '/Movimentacao/CalculaHora?id=' + id + '&dataSaida=' + dataHoraSaida;
        $.ajax({
            url: urlString,
            type: 'GET',
            dataType: "json",
            accepts: 'application/json;charset=utf-8',
            contentType: "application/json;charset=utf-8",
            success: function (result, status, request) {
                console.log(result);
                $("#permanencia").val(result.Permanencia);
                $("#qtde").val(result.Quantidade);
                $("#total").val((result.Total).duasCasas());
            },
            error: function (request, status, erro) {
                console.log(request);
                console.log(status);
                console.log(erro);
            }
        });
    }
}

var movimentacao = new Movimentacao();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
};


