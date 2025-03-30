@model AtividadeExtenso.Controllers.Result

@{
	<h1>Bem-Vindo ao sistema de conversão de números!</ h1 >
}

< hr />

< form method = "post"
asp - action = "Index"
asp asp-controller="Teste">

	<input type="text" id="texto" name="texto" />
	<input type="submit" value="Postar" />

</form>

<hr/>

@if (!string.IsNullOrEmpty(Model.Texto))
{
	<div>
		<span>Texto Convertido: </ span >

        < strong > @Model.Texto </ strong >

    </ div >
}