var setoresConfig = {
	adicionarSetores: function (setores) {
		var clienteId = $('#selectClientes').val();
		var JSetores = $('#selectSetores');

		for(item of setores) {

			var element = document.createElement('option');

			if (item.ClienteId == clienteId) {
				element.text = item.Descricao;
				element.value = item.Id;

				JSetores[0].add(element);
			}
		};
	},

	escolherOptionSelection: function(sel, el){
	    sel.val(el.val());
	},

	escolherSetorSelecionado: function(el){
	    $('#selectSetores').val(el.val());
	},

	escolherClientesSelecionado: function(el){
	    $('#selectClientes').val(el.val());
	},

	adicionarEventoOnChange: function(setores){
		$('#selectClientes').on("change", function (ev) {
            debugger;
			var JSetores = $('#selectSetores');

			setoresConfig.limparSetores();
			debugger;
			var clienteId = ev.currentTarget.value;

			var element = document.createElement('option');

			element.text = "Selecione o setor do Cliente";
			element.value = "";
			JSetores[0].add(element)

				for(item of setores) {

					var element = document.createElement('option');

					if (item.ClienteId == clienteId) {
						element.text = item.Descricao;
						element.value = item.Id;

						JSetores[0].add(element);
					}
				};
		});
	},

	DesabilitarSetoresOnChange: function(el){
	    $('#selectSetores').on("change", function (ev) {
	        $('#selectSetores').val(el.val());
	    });
	},

	DesabilitarClientesOnChange: function (el) {
	    $('#selectClientes').on("change", function (ev) {
	        $('#selectClientes').val(el.val());
	    });
	},

	DesabilitarSelectOnChange: function (sel, el) {
	    sel.on("change", function (ev) {
	       sel.val(el.val());
	    });
	},

	limparSetores: function () {
		var JSetores = $('#selectSetores option');

		JSetores.each(function (cont, item) {
			$('#selectSetores')[0].remove(item);
		});
	},

	adicionarOnClickBuscarChamado: function () {
	    debugger;
	    var btnBuscar = $('#idBuscarChamado');

	    btnBuscar.on("click", function (ev) {
	        if (!$('#idChamadoBusca').val()) {
	            $('#idChamadoBusca-error').text('Selecione um id de Chamado');
	            return;
	        }
	        debugger;
	        setoresConfig.limparCamposChamado();
	        $.ajax({
	            url: '../Chamado/BuscarChamado',
                type: 'POST',
	            data: { ChamadoId: $('#idChamadoBusca').val() },
	            success: function (result) {
	                setoresConfig.exibirFormChamado();
	                setoresConfig.preencherCamposChamado(result);
	                console.log(result);
	                debugger;
	            }, error: function (result) {
	                if (result) {
	                    setoresConfig.esconderFormChamado();
	                    $('#idChamadoBusca').addClass('input-validation-error');
	                    $('#idChamadoBusca-error').text(result.statusText);
	                }
	                console.log('erro');
                }
	        });
	    });
	},

	preencherCamposChamado: function (result) {
	    result = JSON.parse(result);

	    $('#IdChamado').val(result.Id);

	    $('#idNomeSolicitante').text(result.NomeSolicitante);
	    $('#idDescricao').text(result.Descricao);
	    debugger;

	    var option;
	    if (result.Clientes) {
	        for(var el of result.Clientes) {
	            if (el.Value == result.ClienteSelecionado) {
	                $('#idCliente').text(el.Text);
                    break;
	            }
	        }
	    }

	    if (result.SetoresList) {
	        for(var el of result.SetoresList) {
	            if (el.Value == result.Setor) {
	                $('#idSetor').text(el.Text);
	                break;
	            }
	        }
	    }

	    if (result.StatusList) {
	        for(var el of result.StatusList) {
	            if (el.Value == result.StatusSelecionado) {
	                $('#idStatus').text(el.Text);
	                break;
	            }
	        }
	    }
	    
	    //$('#ChamadoViewModel_NomeSolicitante').val(result.NomeSolicitante);
	    //$('#ChamadoViewModel_NomeSolicitante').val(result.NomeSolicitante);
	    $('#idDataAbertura').text(result.Data);
	},

	limparCamposChamado: function () {
	    $('#idNomeSolicitante').text("");
	    $('#idDescricao').text("");
	    $('#idCliente').text("");
	    $('#idSetor').text("");
	    $('#idStatus').text("");
	    $('#idDataAbertura').text("");
	    $('#idChamadoBusca-error').text("");
	    $('#idChamadoBusca').removeClass('input-validation-error')
	},

	configurarEnvioFormulario: function () {
	    $('#idSubmit').on("click", function (ev) {
	        ev.preventDefault();
	        
	        if ($("#VincularChamado:checked").length > 0 && !$("#idNomeSolicitante").text()) {
	            $('#idChamadoBusca').addClass('input-validation-error');
	            $('#idChamadoBusca-error').text('Selecione o chamado para salvar OS');
	            $("#idChamadoBusca").focus();
	            return;
	        }

	        $("#idForm").submit();
	    })
	},

	configurarVinculoChamado: function () {
	    $('#VincularChamado').on('change', function (ev) {
	        if (!$('#VincularChamado:checked').length) {
	            setoresConfig.limparCamposChamado();
	            $("#idChamadoBusca").val("");

	            $("#IdChamado").val("");

	            $('#idFormBuscarChamado').hide({ duration: 600 });
	            $('#idFormChamado').hide({ duration: 600 });
	        } else {
	            $('#idFormBuscarChamado').show({ duration: 600 });
	            //$('#idFormChamado').show({ duration: 600 });
	        }
	    });
	},

	exibirFormChamado: function () {
	    $('#idFormChamado').show({ duration: 600 });
	},

	esconderFormChamado: function () {
        $('#idFormChamado').hide({duration: 600});
	}
    
}