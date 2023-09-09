﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Catalogo.Dominio;
using Catalogo.Negocio;

namespace Catalogo.UI
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulo;
        public frmArticulos()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            CargarImagen(seleccionado.Imagen.ImagenUrl);
        }
        private void CargarImagen(string Imagen)
        {
            try
            {
                pbxArticulo.Load(Imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROGVlwDhbC-6RixbdgEwDrABJ6BD3hhM2eJA&usqp=CAU");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
            cargar();
        }

        private void cargar()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                listaArticulo = articuloNegocio.Listar();
                dgvArticulos.DataSource = listaArticulo;
                dgvArticulos.Columns["id"].Visible = false;
                dgvArticulos.Columns["Imagen"].Visible = false;
                CargarImagen(listaArticulo[0].Imagen.ImagenUrl);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmBajaArticulo bajaArticulo = new frmBajaArticulo();
            bajaArticulo.ShowDialog();
            cargar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if(dgvArticulos.CurrentCell is null)
            {
                MessageBox.Show("Debe Seleccionar un Artículo");
            }
            else
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
        }
    }
}
