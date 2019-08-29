//=================================================================================================
//=================================================================================================
//========================== Funções javascript úteis para manipulação numérica ===================
//=================================================================================================
//=================================================================================================
  //Retorna código do evento na tecla em vários navegadores. Se não conseguir retorna -1
  function capturarCodigoDeEvento(e)
  {
    var code = -1;
	if (!e) 
	    var e = window.event;
	if (e.keyCode) 
	    code = e.keyCode;
	else if (e.which) 
	    code = e.which;
	return code;
  }

  //Testa se é número de 0 a 9
  function isDigit (c){ return ((c >= "0") && (c <= "9"))  }  
  //Testa se é número inteiro
  function isInteger (s) {
    var i;
    if (s.length < 1) return false;
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (!isDigit(c)) return false;
    }
    return true;
  }
  //Testa se é número real(português)
  function isReal (s) {
    var n = "";
    //se houver mais de uma vírgula retorna falso...
    if (s.indexOf(",") != s.lastIndexOf(",")) return false;
    //retorna falso caso a vírgula esteja na primeira ou na última posição da string...
    if ((s.indexOf(",") == 0) || (s.lastIndexOf(",") == (s.length-1))) return false;
    //retira a vírgula que possa existir na string...
    for (var i = 0; i < s.length; i++) { if (s.charAt(i) != ",") n += s.charAt(i); }
    //retorna verdadeiro se o restante da string for um número...
    return isInteger(n);
  }
/*
 * Só permite digitar número vírgula, backspace, esc e enter.
 *
 * @param edt input que será validado
 * @param e palavra reservada "event" que receberá o código do evento da tecla
 * Exemplo da chamada: onkeypress="return txtNumeroKeyPress(this, event);"
 */ 
  function txtNumeroKeyPress(edt, e) {
    
    keyCode = capturarCodigoDeEvento(e);
    //se já existe uma vírgula e se está tentando colocar outra, retorna falso...
    if (keyCode == 44 && edt.value.indexOf(",") >= 0) return false;
    
    //...senão retorna true se for um número, uma vírgula ou se se está teclando ENTER.
    return ((keyCode >= 48 && keyCode <= 57) || keyCode == 44
      || keyCode == 13 || keyCode == 9 || keyCode == 8 );
  }
  
/*
 * Só permite digitar número backspace, esc e enter.
 *
 * @param edt input que será validado
 * @param e palavra reservada "event" que receberá o código do evento da tecla
 * Exemplo da chamada: onkeypress="return txtInteiroKeyPress(this, event);"
 */ 
  function txtInteiroKeyPress(edt,e) {
    keyCode = capturarCodigoDeEvento(e);
    //se é um número ou se se está teclando ENTER, retorna true.
    return ((keyCode >= 48 && keyCode <= 57) || keyCode == 13 || keyCode == 9 || keyCode == 8);
  }

 /*
 * Recebe numerador, denominador e coloca no input text receptor o valor da divisão.
 *
 * @param numerador mês
 * @param denominador ano
 * @param receptor ano
 */
  function dividir(numerador,denominador,receptor)
  {
    var valorNumerador = numerador.value;
    var valorDenominador = denominador.value;
    
    if (!isReal(valorNumerador))
    {
        receptor.value = '';
        return;
    }        
    if (!isReal(valorDenominador))
    {
        receptor.value = '';
        return;
    }
    valorNumerador = valorNumerador.replace(',','.');
    valorDenominador = valorDenominador.replace(',','.');
    var numeradorFinal = parseFloat(valorNumerador);
    var denominadorFinal = parseFloat(valorDenominador);
    resultado = numeradorFinal/denominadorFinal;
    receptor.value = resultado.toString().replace('.',',');
  }
   /*
 * Recebe 2 valores, multiplica-os e coloca no input text receptor o resultado da multiplicação.
 *
 * @param inputUm input text com 1º valor a ser multiplicado
 * @param inputDois input text com 2º valor a ser multiplicado
 * @param receptor input text que receberá o resultado
 */
  function multiplicar(inputUm,inputDois,receptor)
  {
    var valorUm = inputUm.value;
    var valorDois = inputDois.value;
    
    if (!isReal(valorUm))
    {
        receptor.value = '';
        return;
    }        
    if (!isReal(valorDois))
    {
        receptor.value = '';
        return;
    }
    valorUm = valorUm.replace(',','.');
    valorDois = valorDois.replace(',','.');
    var valorUmFinal = parseFloat(valorUm);
    var valorDoisFinal = parseFloat(valorDois);
    resultado = valorUmFinal*valorDoisFinal;
    receptor.value = resultado.toString().replace('.',',');
  }
   /*
 * Recebe 2 valores, multiplica-os e coloca no input text receptor o resultado da multiplicação.
 *
 * @param inputUm input text com 1º valor a ser multiplicado
 * @param inputDois input text com 2º valor a ser multiplicado
 * @param receptor input text que receberá o resultado
 */
    function multiplicarDois(inputUm,inputDois,receptor,receptor_dois)
  {
    var valorUm = inputUm.value;
    var valorDois = inputDois.value;
    
    if (!isReal(valorUm))
    {
        receptor.value = '';
        return;
    }        
    if (!isReal(valorDois))
    {
        receptor.value = '';
        return;
    }
    valorUm = valorUm.replace(',','.');
    valorDois = valorDois.replace(',','.');
    var valorUmFinal = parseFloat(valorUm);
    var valorDoisFinal = parseFloat(valorDois);
    resultado = valorUmFinal*valorDoisFinal;
    receptor.value = resultado.toString().replace('.',',');
    receptor_dois.value = resultado.toString().replace('.',',');
  }
//=================================================================================================
//=================================================================================================
//========================== Funções uteis para manipulação de datas ==============================
//=================================================================================================
//=================================================================================================

//// Formato o campo data "pCampo" passado como parâmetro no formato dd/mm/aaaa
//function formatarCampoData(pCampo, pEvento, pInMesAno) {
//	var vlCampo = pCampo.value;
//	var tam = vlCampo.length;

//	if (isTeclaFuncional(pEvento)) {
//		return;
//	}

//	if (pEvento.keyCode == 111) {
//		if (vlCampo.length != 3 && vlCampo.length != 6) {
//			pCampo.value = vlCampo.substr(0, tam - 1);
//		}
//		return;
//	}

//	var filtro = /^([0-9\/])*$/;
//	if (!filtro.test(vlCampo)) {
//		pCampo.value = vlCampo.substr(0, tam - 1);
//		pCampo.select();
//		alert("Data inválida!");
//		focarCampo(pCampo);
//		return;
//	}

//	if (vlCampo.length == 2) {
//		vlCampo = vlCampo + '/';
//		pCampo.value = vlCampo;
//	}
//	if (vlCampo.length == 5) {
//		if (pInMesAno) {
//			if (vlCampo.substr(3, 4) <= 12) {
//				vlCampo = vlCampo + '/';
//				pCampo.value = vlCampo;
//			}
//		} else {
//			vlCampo = vlCampo + '/';
//			pCampo.value = vlCampo;
//		}
//	}
//	if (pInMesAno && vlCampo.length >= 7) {
//		if (vlCampo.substr(3, 4) > 12) {
//			isCampoDataValido(pCampo, null);
//		}
//	}
//	if (vlCampo.length == 10) {
//		isCampoDataValido(pCampo, null);
//	}
//}
/*
 * Acrescenta máscara de data e não permite a digitação de caracteres inválidos para data.
 *
 * @param edt input que receberá a máscara
 * @param e palavra reservada "event" que receberá o código do evento
 * Exemplo da chamada: onkeypress="return mascaraData(this, event);"
 */ 
function mascaraData(edt, e) {
    keyCode = capturarCodigoDeEvento(e);
    if ((keyCode < 48 || keyCode > 57) 
        && (keyCode != 13) && (keyCode != 9) && (keyCode != 8)) 
        return false;
    if (edt.value.length == 2 || edt.value.length == 5) edt.value += "/";
        return true;
}

/*
 * Recebe mês e ano e retorna número de dias do mês.
 *
 * @param mes mês
 * @param ano ano
 */
function numdias(mes,ano) {
   if((mes<8 && mes%2==1) || (mes>7 && mes%2==0)) return 31;
   if(mes!=2) return 30;
   if(ano%4==0) return 29;
   return 28;
}
/*
 * Recebe data no format dd/MM/aaaa e valida retornando true ou false.
 *
 * @param data dana no formato dd/MM/aaaa
 */
function validaData(data) {
    if(data.length < 6 || data.length > 10) return false;
    pos0 = data.indexOf("/");
    if(pos0 == -1) return false;
    pos1 = data.indexOf("/", pos0 + 1);
    if(pos1 == -1) return false;
    if(data.indexOf("/", pos1 + 1) != -1) return false;
    dia = data.substring(0,pos0);
    dia = (dia.charAt(0) == "0") ? dia.charAt(dia.length - 1) : dia
    mes = data.substring(pos0 + 1, pos1);
    mes = (mes.charAt(0) == "0") ? mes.charAt(mes.length - 1) : mes
    ano = data.substring(pos1 + 1, data.length);
    ano = (ano.length == 2) ? ( (ano.charAt(0) == "0") ? "20" + ano : "19" + ano ) : ano
    if(isNaN(dia) || isNaN(mes) || isNaN(ano)) return false;
    if(parseInt(ano) >= 0 && parseInt(ano) < 1900) return false;
    if(parseInt(ano) > 2100 || parseInt(ano) < 0 || parseInt(mes) > 12 || parseInt(mes) < 1) return false;
    numero = ((parseInt(ano) - 1884) / 4)
    if(numero == Math.floor(numero))
    { dias = "312931303130313130313031"; }
    else
    { dias = "312831303130313130313031"; }
    diamax = parseInt(dias.substring((mes-1)*2,((mes-1)*2)+2));
    if(parseInt(dia) < 1 || parseInt(dia) > diamax) return false;
    return true;
  }
/*
 * Pega data em string no formato dd/MM/aaaa e soma com outro número representando número de dias.
 *
 * @param inputData input text com a data
 * @param inputDias input text com o número de dias a ser somado da data
 * @param inputResultado input text que recebe data resultado.
 * Depende das funções validaData(), numdias() e de isInteger()
 */
function somaDias(inputData, inputDias,inputResultado) {
  var data = inputData.value;
  if (inputDias.value == '' || !isInteger(inputDias.value))
    return;
  if (data == '' || !validaData(data))
    return;
  var dias = parseInt(inputDias.value);
  data = data.split('/');
  if (data[0]=='08')
    data[0] = '8';
  else if (data[0]=='09')
    data[0] = '9';
  if (data[1]=='08')
    data[1] = '8';
  else if (data[1]=='09')
    data[1] = '9';
  var dia = parseInt(data[0]);
  var diafuturo = dia + (dias-1);
  var mes = parseInt(data[1]);
  var ano = parseInt(data[2]);
  while(diafuturo>numdias(mes,ano)) {
      diafuturo-=numdias(mes,ano);
      mes++;
      if(mes>12) {
          mes=1;
          ano++;
      }
  }

  if(diafuturo<10) diafuturo='0'+diafuturo;
  if(mes<10) mes='0'+mes;
  inputResultado.value = diafuturo+"/"+mes+"/"+ano;
}
function Dia(Data_DDMMYYYY)
{
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra!= -1)
    {
        dia = string_data.substring(0,posicao_barra);
        return dia;
    }
    else
    {
        return false;
    }
}

function Mes(Data_DDMMYYYY)
{
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra!= -1)
    {
        dia = string_data.substring(0,posicao_barra);
        string_mes = string_data.substring(posicao_barra+1,string_data.length);
        posicao_barra = string_mes.indexOf("/");
        if (posicao_barra!= -1)
        {
            mes = string_mes.substring(0,posicao_barra);
            mes = Math.floor(mes);
            return mes;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}

function Ano(Data_DDMMYYYY)
{
    string_data = Data_DDMMYYYY.toString();
    posicao_barra = string_data.indexOf("/");
    if (posicao_barra!= -1)
    {
        dia = string_data.substring(0,posicao_barra);
        string_mes = string_data.substring(posicao_barra+1,string_data.length);
        posicao_barra = string_mes.indexOf("/");
        if (posicao_barra!= -1)
        {
            mes = string_mes.substring(0,posicao_barra);
            mes = Math.floor(mes);
            ano = string_mes.substring(posicao_barra+1,string_mes.length);
            return ano;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}
function calculaDiferencaEntreDias(input1,input2,inputResultado){
    
    var data1_DDMMYYYY = input1.value;
    var data2_DDMMYYYY = input2.value;
    
    Var_Dia1=Dia(data1_DDMMYYYY);
    Var_Mes1=Mes(data1_DDMMYYYY);
    Var_Mes1=Math.floor(Var_Mes1)-1;
    Var_Ano1=Ano(data1_DDMMYYYY);
    var data1 = new Date(Var_Ano1,Var_Mes1,Var_Dia1);

    Var_Dia2=Dia(data2_DDMMYYYY);
    Var_Mes2=Mes(data2_DDMMYYYY);
    Var_Mes2=Math.floor(Var_Mes2)-1;
    Var_Ano2=Ano(data2_DDMMYYYY);
    var data2 = new Date(Var_Ano2,Var_Mes2,Var_Dia2);

    var diferenca = data1.getTime() - data2.getTime();
    var diferenca = Math.floor(diferenca / (1000 * 60 * 60 * 24));
    inputResultado.value = diferenca;
}
//=================================================================================================
//=================================================================================================
//===================================== Validações de campos ======================================
//=================================================================================================
//=================================================================================================
/**
 * Testa se a String pCpfCnpj fornecida é um CPF ou CNPJ válido.
 * Se a String tiver uma quantidade de dígitos igual ou inferior
 * a 11, valida como CPF. Se for maior que 11, valida como CNPJ.
 * Qualquer formatação que não seja algarismos é desconsiderada.
 * A assinatura segue a exigência do componente custom validator do ASP.NET.
 *
 * @param source
 * @param arguments objeto com os argumentos com os quais interagiremos
 * (por exemplo o valor do campo a ser validado(Value) e o parâmetro que diz se é válido ou não(IsValid))
 * 	String fornecida para ser testada.
 * @return <code>true</code> se a String fornecida for um CPF ou CNPJ válido.
 */
function isCpfCnpj(source, arguments)
{
    var pCpfCnpj = arguments.Value;
	var numero = pCpfCnpj.replace(/\D/g, "");
	if (numero.length > NUM_DIGITOS_CPF)
		arguments.IsValid = isCnpj(pCpfCnpj);
	else
		arguments.IsValid = isCpf(pCpfCnpj);
} //isCpfCnpj




/**
 * @author Márcio d'Ávila
 * @version 1.01, 2004
 *
 * PROTÓTIPOS:
 * método String.lpad(int pSize, char pCharPad)
 * método String.trim()
 *
 * String unformatNumber(String pNum)
 * String formatCpfCnpj(String pCpfCnpj, boolean pUseSepar, boolean pIsCnpj)
 * String dvCpfCnpj(String pEfetivo, boolean pIsCnpj)
 * boolean isCpf(String pCpf)
 * boolean isCnpj(String pCnpj)
 * boolean isCpfCnpj(String pCpfCnpj)
 */


NUM_DIGITOS_CPF  = 11;
NUM_DIGITOS_CNPJ = 14;
NUM_DGT_CNPJ_BASE = 8;


/**
 * Adiciona método lpad() à classe String.
 * Preenche a String à esquerda com o caractere fornecido,
 * até que ela atinja o tamanho especificado.
 */
String.prototype.lpad = function(pSize, pCharPad)
{
	var str = this;
	var dif = pSize - str.length;
	var ch = String(pCharPad).charAt(0);
	for (; dif>0; dif--) str = ch + str;
	return (str);
} //String.lpad


/**
 * Adiciona método trim() à classe String.
 * Elimina brancos no início e fim da String.
 */
String.prototype.trim = function()
{
	return this.replace(/^\s*/, "").replace(/\s*$/, "");
} //String.trim


/**
 * Elimina caracteres de formatação e zeros à esquerda da string
 * de número fornecida.
 * @param String pNum
 * 	String de número fornecida para ser desformatada.
 * @return String de número desformatada.
 */
function unformatNumber(pNum)
{
	return String(pNum).replace(/\D/g, "").replace(/^0+/, "");
} //unformatNumber


/**
 * Formata a string fornecida como CNPJ ou CPF, adicionando zeros
 * à esquerda se necessário e caracteres separadores, conforme solicitado.
 * @param String pCpfCnpj
 * 	String fornecida para ser formatada.
 * @param boolean pUseSepar
 * 	Indica se devem ser usados caracteres separadores (. - /).
 * @param boolean pIsCnpj
 * 	Indica se a string fornecida é um CNPJ.
 * 	Caso contrário, é CPF. Default = false (CPF).
 * @return String de CPF ou CNPJ devidamente formatada.
 */
function formatCpfCnpj(pCpfCnpj, pUseSepar, pIsCnpj)
{
	if (pIsCnpj==null) pIsCnpj = false;
	if (pUseSepar==null) pUseSepar = true;
	var maxDigitos = pIsCnpj? NUM_DIGITOS_CNPJ: NUM_DIGITOS_CPF;
	var numero = unformatNumber(pCpfCnpj);

	numero = numero.lpad(maxDigitos, '0');
	if (!pUseSepar) return numero;

	if (pIsCnpj)
	{
		reCnpj = /(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/;
		numero = numero.replace(reCnpj, "$1.$2.$3/$4-$5");
	}
	else
	{
		reCpf  = /(\d{3})(\d{3})(\d{3})(\d{2})$/;
		numero = numero.replace(reCpf, "$1.$2.$3-$4");
	}
	return numero;
} //formatCpfCnpj


/**
 * Calcula os 2 dígitos verificadores para o número-efetivo pEfetivo de
 * CNPJ (12 dígitos) ou CPF (9 dígitos) fornecido. pIsCnpj é booleano e
 * informa se o número-efetivo fornecido é CNPJ (default = false).
 * @param String pEfetivo
 * 	String do número-efetivo (SEM dígitos verificadores) de CNPJ ou CPF.
 * @param boolean pIsCnpj
 * 	Indica se a string fornecida é de um CNPJ.
 * 	Caso contrário, é CPF. Default = false (CPF).
 * @return String com os dois dígitos verificadores.
 */
function dvCpfCnpj(pEfetivo, pIsCnpj)
{
	if (pIsCnpj==null) pIsCnpj = false;
	var i, j, k, soma, dv;
	var cicloPeso = pIsCnpj? NUM_DGT_CNPJ_BASE: NUM_DIGITOS_CPF;
	var maxDigitos = pIsCnpj? NUM_DIGITOS_CNPJ: NUM_DIGITOS_CPF;
	var calculado = formatCpfCnpj(pEfetivo, false, pIsCnpj);
	calculado = calculado.substring(2, maxDigitos);
	var result = "";

	for (j = 1; j <= 2; j++)
	{
		k = 2;
		soma = 0;
		for (i = calculado.length-1; i >= 0; i--)
		{
			soma += (calculado.charAt(i) - '0') * k;
			k = (k-1) % cicloPeso + 2;
		}
		dv = 11 - soma % 11;
		if (dv > 9) dv = 0;
		calculado += dv;
		result += dv
	}

	return result;
} //dvCpfCnpj


/**
 * Testa se a String pCpf fornecida é um CPF válido.
 * Qualquer formatação que não seja algarismos é desconsiderada.
 * @param String pCpf
 * 	String fornecida para ser testada.
 * @return <code>true</code> se a String fornecida for um CPF válido.
 */
function isCpf(pCpf)
{
	var numero = formatCpfCnpj(pCpf, false, false);
	var base = numero.substring(0, numero.length - 2);
	var digitos = dvCpfCnpj(base, false);
	var algUnico, i;

	// Valida dígitos verificadores
	if (numero != base + digitos) return false;

	/* Não serão considerados válidos os seguintes CPF:
	 * 000.000.000-00, 111.111.111-11, 222.222.222-22, 333.333.333-33, 444.444.444-44,
	 * 555.555.555-55, 666.666.666-66, 777.777.777-77, 888.888.888-88, 999.999.999-99.
	 */
	algUnico = true;
	for (i=1; i<NUM_DIGITOS_CPF; i++)
	{
		algUnico = algUnico && (numero.charAt(i-1) == numero.charAt(i));
	}
	return (!algUnico);
} //isCpf


/**
 * Testa se a String pCnpj fornecida é um CNPJ válido.
 * Qualquer formatação que não seja algarismos é desconsiderada.
 * @param String pCnpj
 * 	String fornecida para ser testada.
 * @return <code>true</code> se a String fornecida for um CNPJ válido.
 */
function isCnpj(pCnpj)
{
	var numero = formatCpfCnpj(pCnpj, false, true);
	var base = numero.substring(0, NUM_DGT_CNPJ_BASE);
	var ordem = numero.substring(NUM_DGT_CNPJ_BASE, 12);
	var digitos = dvCpfCnpj(base + ordem, true);
	var algUnico;

	// Valida dígitos verificadores
	if (numero != base + ordem + digitos) return false;

	/* Não serão considerados válidos os CNPJ com os seguintes números BÁSICOS:
	 * 11.111.111, 22.222.222, 33.333.333, 44.444.444, 55.555.555,
	 * 66.666.666, 77.777.777, 88.888.888, 99.999.999.
	 */
	algUnico = numero.charAt(0) != '0';
	for (i=1; i<NUM_DGT_CNPJ_BASE; i++)
	{
		algUnico = algUnico && (numero.charAt(i-1) == numero.charAt(i));
	}
	if (algUnico) return false;

	/* Não será considerado válido CNPJ com número de ORDEM igual a 0000.
	 * Não será considerado válido CNPJ com número de ORDEM maior do que 0300
	 * e com as três primeiras posições do número BÁSICO com 000 (zeros).
	 * Esta crítica não será feita quando o no BÁSICO do CNPJ for igual a 00.000.000.
	 */
	if (ordem == "0000") return false;
	return (base == "00000000"
		|| parseInt(ordem, 10) <= 300 || base.substring(0, 3) != "000");
} //isCnpj

//=================================================================================================
//=================================================================================================
//========================== Conjunto de funções para colocar número por extenso ==================
//=================================================================================================
//=================================================================================================
/*
 *Função que retorna número por extenso. Tem por parâmetro o número em si e um booleano dizendo se é moeda
 *(em reais) ou se é número normal e um segundo parâmetro booleano dizendo se o retorno deve ser em caixa alta ou não.
 *
 */
    function extenso(valor, blnmoeda, isMaiusculo)
    {
            var intponto;
            var inteiro, decimal;
            var strmoedasingular, strmoedaplural;
            var retorno;

            if (!isReal(valor))
            {
                    //alert(valor + ' não é um número válido!');  
                    return '';
            }

            intponto = valor.indexOf(',');

            if (intponto < 0 )
            {
                    inteiro = zeros(valor,15);
                    decimal = zeros(0,15);
            }
            else
            {
                    
                    decimal = valor.substring(intponto + 1, valor.length)
                    if(decimal.length > 2)
                            decimal = decimal.substring(0,2);
                    if (decimal.length == 1)
                            decimal = decimal + '0';
                    decimal = zeros(decimal,15);

                    inteiro = zeros(valor.substring(0, intponto),15);
            }

            retorno = mil(inteiro,15,'trilhão','trilhões');

            if ((inteiro.substr(0,3) > 0) && (inteiro.substr(3,12) > 0))
            {
                    if (inteiro.substr(3,10) == 0 ||
                            (inteiro.substr(3,9) + inteiro.substr(13,2)) == 0 ||
                            (inteiro.substr(3,4) + inteiro.substr(9,6)) == 0 ||
                            (inteiro.substr(3,3) + inteiro.substr(7,8)) == 0 ||
                            (inteiro.substr(3,6) + inteiro.substr(10,5)) == 0 ||
                            (inteiro.substr(3,7) + inteiro.substr(12,3)) == 0 ||
                            (inteiro.substr(3,1) + inteiro.substr(6,9)) == 0 ||
                            inteiro.substr(4,11) == 0)
                            retorno = retorno + ' e ';
                    else
                            retorno = retorno + ', ';
            }

            retorno = retorno + mil(inteiro,12,'bilhão','bilhões');

            if (inteiro.substr(3,3) > 0 && inteiro.substr(6,9) > 0)
            {
                    if (inteiro.substr(6,7) == 0 ||
                            (inteiro.substr(6,6) + inteiro.substr(13,2)) == 0 ||
                            (inteiro.substr(6,4) + inteiro.substr(12,3)) == 0 ||
                            (inteiro.substr(6,3) + inteiro.substr(10,5)) == 0 ||
                            (inteiro.substr(6,1) + inteiro.substr(9,6)) == 0 ||
                            inteiro.substr(7,8) == 0)
                            retorno = retorno + ' e ';
                    else
                            retorno = retorno + ', ';
            }

            retorno = retorno + mil(inteiro,9,'milhão','milhões');

            if (inteiro.substr(6,3) > 0 && inteiro.substr(9,6) > 0)
            {
                    if (inteiro.substr(9,4) == 0 ||
                            (inteiro.substr(9,3) + inteiro.substr(13,2)) == 0 ||
                            (inteiro.substr(9,1) + inteiro.substr(12,3)) == 0 ||
                            inteiro.substr(10,5) == 0)
                            retorno = retorno + ' e ';
                    else
                            retorno = retorno + ', ';
            }

            retorno = retorno + mil(inteiro,6,'mil','mil');

            if (inteiro.substr(9,3) > 0 && inteiro.substr(12,3) > 0)
            {
                    if (inteiro.substr(13,2) == 0 ||
                            inteiro.substr(12,2) == 0 ||
                            inteiro.substr(12,1) == 0)
                            retorno = retorno + ' e ';
                    else
                            retorno = retorno + ', ';
            }

            if (blnmoeda)
            {
                    strmoedasingular = 'real';
                    strmoedaplural = 'reais';
            }
            else
            {
                    strmoedasingular = '';
                    strmoedaplural = '';
            }

            retorno = retorno + mil(inteiro,3,strmoedasingular,strmoedaplural);

            if (decimal > 0)
            {
                    if (inteiro > 0)
                            retorno = retorno + ' e '
                     retorno = retorno + cem(decimal, 2, 'centavo', 'centavos');
            }
            if (isMaiusculo)
            {
                retorno = retorno.toUpperCase();
            }
            
            return retorno;
    }

    function mil(numero,centena,unidadesingular,unidadeplural)
    {
            var retorno;

            retorno = '';

            if (numero.substr(15 - centena,1) > 0)
            {
                    if (numero.substr(15 - centena,1) == 1)
                    {
                            if (numero.substr(16 - centena,2) == 0)
                                    retorno = 'cem'
                            else
                                    retorno = 'cento'
                    }
                    else
                    {
                            switch (numero.substr(15 - centena,1))
                            {
                                    case '2':
                                            retorno = 'duzentos';
                                            break;
                                    case '3':
                                            retorno = 'trezentos';
                                            break;
                                    case '4':
                                            retorno = 'quatrocentos';
                                            break;
                                    case '5':
                                            retorno = 'quinhentos';
                                            break;
                                    case '6':
                                            retorno = 'seiscentos';
                                            break;
                                    case '7':
                                            retorno = 'setecentos';
                                            break;
                                    case '8':
                                            retorno = 'oitocentos';
                                            break;
                                    case '9':
                                            retorno = 'novecentos';
                                            break;                          
                            }
                    }
                    if (numero.substr(16 - centena,2) > 0)
                            retorno = retorno + ' e ';
            }

            retorno = retorno + cem(numero, centena -1, unidadesingular, unidadeplural)

            return retorno;
    }

    function cem(numero, dezena, unidadesingular, unidadeplural)
    {
            var retorno;

            retorno = '';

            if (numero.substr(15 - dezena,1) > 1)
            {
                    switch (numero.substr(15 - dezena,1))
                    {
                            case '2':
                                    retorno = 'vinte';
                                    break;
                            case '3':
                                    retorno = 'trinta';
                                    break;
                            case '4':
                                    retorno = 'quarenta';
                                    break;
                            case '5':
                                    retorno = 'cinqüenta';
                                    break;
                            case '6':
                                    retorno = 'sessenta';
                                    break;
                            case '7':
                                    retorno = 'setenta';
                                    break;
                            case '8':
                                    retorno = 'oitenta';
                                    break;
                            case '9':
                                    retorno = 'noventa';
                                    break;                          
                    }                              
                    if (numero.substr(16 - dezena,1) > 0)                
                    {
                            retorno = retorno + ' e ';
                            switch (numero.substr(16 - dezena,1))
                            {
                                    case '1':
                                            retorno = retorno + 'um';
                                            break;
                                    case '2':
                                            retorno = retorno + 'dois';
                                            break;
                                    case '3':
                                            retorno = retorno + 'três';
                                            break;
                                    case '4':
                                            retorno = retorno + 'quatro';
                                            break;
                                    case '5':
                                            retorno = retorno + 'cinco';
                                            break;
                                    case '6':
                                            retorno = retorno + 'seis';
                                            break;
                                    case '7':
                                            retorno = retorno + 'sete';
                                            break;
                                    case '8':
                                            retorno = retorno + 'oito';
                                            break;
                                    case '9':
                                            retorno = retorno + 'nove';
                                            break;
                            }
                    }
            }
            else
            {
                    switch (numero.substr(15 - dezena,2))
                    {
                            case '01':
                                    retorno = 'um';
                                    break;
                            case '02':
                                    retorno = 'dois';
                                    break;
                            case '03':
                                    retorno = 'três';
                                    break;
                            case '04':
                                    retorno = 'quatro';
                                    break;
                            case '05':
                                    retorno = 'cinco';
                                    break;
                            case '06':
                                    retorno = 'seis';
                                    break;
                            case '07':
                                    retorno = 'sete';
                                    break;
                            case '08':
                                    retorno = 'oito';
                                    break;
                            case '09':
                                    retorno = 'nove';
                                    break;
                            case '10':
                                    retorno = 'dez';
                                    break;
                            case '11':
                                    retorno = 'onze';
                                    break;
                            case '12':
                                    retorno = 'doze';
                                    break;
                            case '13':
                                    retorno = 'treze';
                                    break;
                            case '14':
                                    retorno = 'quatorze';
                                    break;
                            case '15':
                                    retorno = 'quinze';
                                    break;
                            case '16':
                                    retorno = 'dezeseis';
                                    break;
                            case '17':
                                    retorno = 'dezesete';
                                    break;
                            case '18':
                                    retorno = 'dezoito';
                                    break;
                            case '19':
                                    retorno = 'dezenove';
                                    break;                                                                                                                                                                                                                                                                                                                                                                  
                    }
            }
            if (dezena == 2)
            {
                    if (numero > 0)
                    {
                            if (numero == 1)
                            {
                                    retorno = retorno + ' ' + unidadesingular;
                            }
                            else
                            {
                                    if (numero.substr(9,6) == 0 ||
                                            numero.substr(6,9) == 0 ||  
                                            numero.substr(3,12) == 0)
                                            retorno = retorno + ' de ' + unidadeplural;
                                    else
                                            retorno = retorno + ' ' + unidadeplural;
                            }
                    }
            }
            else
            {
                    if (numero.substr(14 - dezena,3) > 0)
                    {
                            if (numero.substr(14 - dezena,3) == 1)
                                    retorno = retorno + ' ' + unidadesingular
                            else
                                    retorno = retorno + ' ' + unidadeplural
                    }
            }

            return retorno;
    }

    function zeros(valor, zeros)
    {
            var i, intqtde, strretorno

            intqtde = zeros - valor.length;
            strretorno = valor

            for (i=0; i < intqtde; i++)
            {
                    strretorno = '0' + strretorno

            }
            return strretorno;
    }
    
//=================================================================================================
//=================================================================================================
//========================== Conjunto de funções úteis gerais =====================================
//=================================================================================================
//=================================================================================================

/**
 *
 * Função utilizada para requisitar confirmação nas exclusões    
 */
function confirma(){
    if(window.confirm("Deseja realmente excluir?\n A operação não pode ser desfeita!"))
        return true;
    else
        return false;
}
/**
 * Limita o número de caracteres que um input deve ter, é utilizado em textareas.
 * @param input
 * 	input text(textbox) ou textarea que deve ter seu tamanho limitado.
 * @param maxLength
 * 	número máximo de caracteres que o input deve ter.
 * @param digitado
 * 	id do span ou div ou qualquer tag html contendo o número de caracteres que foi digitado.
 * @param restante
 * 	id do span ou div ou qualquer tag html contendo o número de caracteres que ainda pode ser digitado.
 */
function maxLength(input,maxLength,digitado,restante){
    
    var tamanho = input.value.length;
    var objetoDigitado = document.getElementById(digitado);
    var objetoRestante = document.getElementById(restante);
    var str="";
    str=str+tamanho;
    objetoDigitado.innerHTML = str; 
    objetoRestante.innerHTML = maxLength - str; 
    
    if (tamanho > maxLength){ 
        var aux = input.value; 
        input.value = aux.substring(0,maxLength);
        objetoDigitado.innerHTML = maxLength; 
        objetoRestante.innerHTML = 0;
    }
}
/**
 * Desabilita todas as entradas (select, textarea, input text, input radio, input checkbox, input password),
 * exceto aquele que disparou o evento
 *
 * @param edit
 * 	entrada do tipo "<select>" que disparará o evento e não ficará desabilitada.
 * Exemplo da chamada: onchange="desabilitarEntradas(this);"
 */
function desabilitarEntradas(edit){
	//Só há um formulário em páginas aspx, então nos restringimos a buscar inputs somente nele.
	var formulario = document.getElementsByTagName("form").item(0);
	//captura todos os objetos de tag input
	var elementosInput = formulario.getElementsByTagName("input");
	//captura todos os objetos de tag select
	var elementosSelect = formulario.getElementsByTagName("select");
	//captura todos os objetos de tag textarea
	var elementosTextArea = formulario.getElementsByTagName("textarea");
	//captura o id do select que disparou o evento
	var editId = edit.id;
	var objetoInput;
	var objetoSelect;
	var objetoTextArea;
	var idObjeto;
	//variável que dirá se as entradas do form devem ser desabilitadas(true) ou não(false)
	var isDesabilitado = true;
	//se o select tiver um valor vazio, não devem ser desabilitadas, senão devem ser desabilitadas
	if(edit.options[edit.selectedIndex].value == "" )
	{
		isDesabilitado = false;
	}	
	//varrendo os inputs
	for (var i=0; i< elementosInput.length; i++)
	{
		//pega o id do elemento
		idObjeto = elementosInput[i].id;
		if (idObjeto != "")
		{
		    //alert(idObjeto);
		    //se o id do elemento não for o elemento que disparou o evento prossiga
		    if(idObjeto!=editId)
		    {
			    //pega o elemento para manipulá-lo através de seu id
			    objetoInput = document.getElementById(idObjeto);
			    //Se for input text, password, checkbox, radio(o que exclui button, reset, submit, etc) desabilite
			    if (objetoInput.type.toUpperCase() == "TEXT" || objetoInput.type.toUpperCase() == "PASSOWORD"
				    || objetoInput.type.toUpperCase() == "CHECKBOX" || objetoInput.type.toUpperCase() == "RADIO" )
			    {
				    objetoInput.disabled = isDesabilitado;
			    }
		    }
		}		
	}
	//varrendo os selects
	for (var i=0; i< elementosSelect.length; i++)
	{	
		//pega o id do elemento
		idObjeto = elementosSelect[i].id;
		if (idObjeto != "")
		{
		    //se o id do elemento não for o elemento que disparou o evento prossiga
		    if(idObjeto!=editId)
		    {
			    //pega o elemento para manipulá-lo através de seu id
			    objetoSelect = document.getElementById(idObjeto);
			    /*if(objetoSelect.options[objetoSelect.selectedIndex].value != "" )
			    {
				    for (var i=0; i< objetoSelect.options.length -1; i++)
				    {
					    if(objetoSelect.options[i].value == "")
					    {
						    objetoSelect.options[i].selected = true;
						    break;
					    }
				    }				
			    }*/
			    objetoSelect.disabled = isDesabilitado;
		    }
		}
	}
	//varrendo os textarea's
	for (var i=0; i< elementosTextArea.length; i++)
	{	
		//pega o id do elemento
		idObjeto = elementosTextArea[i].id;
		if (idObjeto != "")
		{
		    //se o id do elemento não for o elemento que disparou o evento prossiga
		    if(idObjeto!=editId)
		    {
			    //pega o elemento para manipulá-lo através de seu id
			    objetoTextArea = document.getElementById(idObjeto);
			    objetoTextArea.disabled = isDesabilitado;
		    }
		}
	}
	
}