using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;

namespace PogoPagApp.Views
{
    public partial class CadastroPage : ContentPage
    {
        private int stepAtual = 1;

        public CadastroPage()
        {
            InitializeComponent();
            AtualizarIndicadorEtapa();
        }

        private async void OnProximoClicked(object sender, EventArgs e)
        {
            if (stepAtual == 1)
            {
                await TransicaoEtapas(step1, step2);
                btnVoltar.IsVisible = true;
                stepAtual++;
            }
            else if (stepAtual == 2)
            {
                await TransicaoEtapas(step2, step3);
                stepAtual++;
            }
            else if (stepAtual == 3)
            {
                await TransicaoEtapas(step3, step4);
                btnProximo.IsVisible = false;
                btnFinalizar.IsVisible = true;
                lblResumo.Text = ObterResumoCadastro();
                stepAtual++;
            }

            AtualizarIndicadorEtapa();
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            if (stepAtual == 2)
            {
                await TransicaoEtapas(step2, step1);
                btnVoltar.IsVisible = false;
                stepAtual--;
            }
            else if (stepAtual == 3)
            {
                await TransicaoEtapas(step3, step2);
                stepAtual--;
            }
            else if (stepAtual == 4)
            {
                await TransicaoEtapas(step4, step3);
                btnProximo.IsVisible = true;
                btnFinalizar.IsVisible = false;
                stepAtual--;
            }

            AtualizarIndicadorEtapa();
        }

        private void OnFinalizarClicked(object sender, EventArgs e)
        {
            DisplayAlert("Cadastro", "Cadastro concluído com sucesso!", "OK");
        }

        private void AtualizarIndicadorEtapa()
        {
            lblStep.Text = $"Etapa {stepAtual} de 4";
        }

        private async Task TransicaoEtapas(StackLayout atual, StackLayout proxima)
        {
            await atual.FadeTo(0, 300);
            atual.IsVisible = false;

            proxima.IsVisible = true;
            await proxima.FadeTo(1, 300);
        }

        private string ObterResumoCadastro()
        {
            return $"Nome: {txtNome.Text}\n" +
                   $"E-mail: {txtEmail.Text}\n" +
                   $"CPF: {txtCPF.Text}\n" +
                   $"Data de Nascimento: {dpNascimento.Date:dd/MM/yyyy}\n" +
                   $"Telefone: {txtTelefone.Text}";
        }

        private void FormatCPF(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;
            if (entry == null || string.IsNullOrWhiteSpace(entry.Text)) return;

            string digits = Regex.Replace(entry.Text, "[^0-9]", "");
            if (digits.Length == 11)
            {
                entry.Text = $"{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6, 3)}-{digits.Substring(9, 2)}";
            }
            else
            {
                entry.Text = "";
                DisplayAlert("Erro", "O CPF deve conter 11 dígitos.", "OK");
            }
        }
    }
}
