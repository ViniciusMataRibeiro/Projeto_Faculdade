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

namespace ProjetoMTA.UI.Cliente
{
    public partial class FormClienteConsulta : FormBaseConsulta
    {
        public List<ClienteDto> a { get; set; }
        public FormClienteConsulta()
        {
            InitializeComponent();
            a = new List<ClienteDto>();
            InitGridBaseConsulta<ClienteDto>(Grid);
        }

        private async void FormClienteConsulta_Load(object sender, EventArgs e)
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
            var obj = (ClienteDto)Grid.CurrentRow?.DataBoundItem;
            if (obj == null) return;

            FormClienteCadastro frm = new FormClienteCadastro("Alterar", obj);
            frm.ShowDialog();
            if (frm.ErroAoGravar) await CarregarGrid();
            else
            {
                //AtualizarGrid(); apenas coloca os valores denovo no grid caso der erro
                bindingSource.ResetBindings(false);
            }
            frm.Dispose();
        }

        private void btExcluir_Click(object sender, EventArgs e)
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
            FormClienteCadastro frm = new FormClienteCadastro("incluir", new ClienteDto());
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
