using DB;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjClientes
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ConnectionSQLite.createDataBaseSQLite("dbcliente.sqlite");
            ConnectionSQLite.CreateTableSQLite("Clientes");
            //Receber as informações que estão na tela:
            Add(GetCliente());
            //Teste Consulta:
            dgvCliente.DataSource = GetAll();

        }

        #region Métodos
        private void Add (Cliente cliente)
        {
            ConnectionSQLite.Add(cliente);
        }

        private DataTable GetAll()
        {
            return ConnectionSQLite.GetAll();
        }

        private Cliente GetCliente()
        {
            return new Cliente()
            {
                Id = int.Parse(txtId.Text),
                Nome = txtNome.Text,
                Telefone = txtTelefone.Text
            };
        }
        #endregion
    }
}
