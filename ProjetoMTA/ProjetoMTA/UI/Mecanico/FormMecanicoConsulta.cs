﻿using DataBase;
using ProjetoMTA.Base;
using ProjetoMTA.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoMTA.UI.Mecanico
{
    public partial class FormMecanicoConsulta : FormBaseConsulta
    {
        public FormMecanicoConsulta()
        {
            InitializeComponent();
        }

        private async void FormMecanicoConsulta_Load(object sender, EventArgs e)
        {
            await CarregarGrid();
        }

        private async Task CarregarGrid()
        {

        }

        private void BtIncluir_Click(object sender, EventArgs e)
        {
            AdicionarDado();
        }
        
        private async void btAlterar_Click(object sender, EventArgs e)
        {
            var obj = (MecanicoDto)Grid.CurrentRow?.DataBoundItem;
            if (obj == null) return;

            FormMecanicoCadastro frm = new FormMecanicoCadastro("Alterar", obj);
            frm.ShowDialog();
            if (frm.ErroAoGravar) await CarregarGrid();
            else
            {
                //AtualizarGrid(); apenas coloca os valores denovo no grid caso der erro
                bindingSource.ResetBindings(false);
            }
            frm.Dispose();
        }

        private async void btExcluir_Click(object sender, EventArgs e)
        {
            var obj = (ClienteDto)Grid.CurrentRow?.DataBoundItem;
            if (obj == null) return;

            try
            {
                if (OficinaMessageBox.Show("Deseja realmente remover este Cliente?", "Delete", OFButtons.YesNo, OFIcon.Question) == DialogResult.No)
                    return;
                //var deletou = await  delete
                //if (deletou)
                //{
                bindingSource.Remove(obj);
                DisplayMessage("Equipamento removido com sucesso", "Dado deletado");
                //}
                //else
                //{
                //throw new Exception("Não foi possível deletar equipamento");
                //}
            }
            catch (Exception x)
            {
                DisplayMessage(x.Message, "Problema ao deletar registro", OFIcon.Warning);
            }
        }

        private void AdicionarDado()
        {
            FormMecanicoCadastro frm = new FormMecanicoCadastro("incluir", new MecanicoDto());
            frm.ShowDialog();
            if (frm.Gravou)
            {
                bindingSource.Add(frm.Dto);
                if (frm.CriarNovo) AdicionarDado();
                else bindingSource.ResetBindings(false);
            }
            frm.Dispose();
        }
    }
}