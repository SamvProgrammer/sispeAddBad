using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISPE_MIGRACION.formularios.PRESTACIONES_ECON.CONTROL_Y_REGISTRO.QUIROGRAFARIO
{
    public partial class validarSituacionLaboral : Form
    {
        public validarSituacionLaboral()
        {
            InitializeComponent();
        }

        private void validarSituacionLaboral_Load(object sender, EventArgs e)
        {

            lblMensaje.Visible = false;

            DateTime fecha1 = DateTime.Now;
            fecha1 = new DateTime(fecha1.Year, fecha1.Month, 15);
            fecha1 = fecha1.AddDays(15);
            fecha1 = new DateTime(fecha1.Year, fecha1.Month, 15);

            DateTime fecha2 = fecha1;
            fecha2 = fecha2.AddDays(-30);
            if (fecha2.Month == 2)
            {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month, 28);
            }
            else
            {
                fecha2 = new DateTime(fecha2.Year, fecha2.Month, 30);
            }

            fe1.Value = fecha1;
            fe2.Value = fecha2;
        }

        private void fe1_ValueChanged(object sender, EventArgs e)
        {
            DateTime fecha = fe1.Value;
            fecha = fecha.AddDays(-30);
            if (fecha.Month == 2)
            {
                fecha = new DateTime(fecha.Year, fecha.Month, 28);
            }
            else
            {
                fecha = new DateTime(fecha.Year, fecha.Month, 30);
            }
            fe2.Value = fecha;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.No == MessageBox.Show("¿Desea efectuar la operación?", "Seleccionar folios", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    MessageBox.Show("Operación cancelada", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                MessageBox.Show("Se va a seleccionar los folios..", "Seleccionando folios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMensaje.Visible = true;
                lblMensaje.Text = "SELECCIONANDO FOLIOS";
                string fechaDescuento = string.Empty;
                DateTime fecha = fe1.Value;
                fechaDescuento = string.Format("{0}-{1}-{2}", fecha.Year, fecha.Month, fecha.Day);
                string query = string.Format("select *,' ' as final from datos.d_ecqdep where f_descuento = '{0}'", fechaDescuento);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                int contador = 1;
                foreach (Dictionary<string, object> item in resultado)
                {

                    string rfc = Convert.ToString(item["rfc"]);
                    DateTime f = new DateTime(); ;
                    lblMensaje.Text = string.Format("SELECCIONANDO PRESTAMOS A DESCONTAL EL DESCUENTO {0}", contador);
                    contador++;
                    query = string.Format("select * from datos.aportaciones where rfc = '{0}'", rfc);
                    List<Dictionary<string, object>> resultado2 = globales.consulta(query);
                    foreach (Dictionary<string, object> item2 in resultado2)
                    {
                        if (Convert.ToString(item2["new_tipo"]).ToUpper() == "AN")
                        {
                            string auxFecha = Convert.ToString(item2["final"]).Replace(" 12:00:00 a. m.", "");
                            string[] arreglo = auxFecha.Split('/');
                            DateTime aux = new DateTime(Convert.ToInt32(arreglo[2]), Convert.ToInt32(arreglo[1]), Convert.ToInt32(arreglo[0]));
                            if (f < aux)
                            {
                                f = aux;
                            }

                        }
                        if (fe2.Value == f) break; //YA SE ENCONTRO LA FECHA SOLICITADA...
                    }
                    item["final"] = string.Format("{0}-{1}-{2}", f.Year, f.Month, f.Day);
                }
                MessageBox.Show("Proceso finalizado\nSe genero " + contador + " aportaciones..", "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Dictionary<string, object>> family = resultado;
                lblMensaje.Visible = false;
                lblMensaje.Text = string.Empty;
                generarReporte(family);
            }
            catch
            {
                MessageBox.Show("Error en el sistema, contactar a informatica", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Cursor = Cursors.Default;
        }




        private void generarReporte(List<Dictionary<string, object>> family)
        {
            MessageBox.Show("Se va a generar el reporte", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult respuesta = svFile.ShowDialog();
            if (respuesta == DialogResult.No) return;

            string rutaPrincipal = svFile.FileName;

            StreamWriter escribir = new StreamWriter(rutaPrincipal);
            
            escribir.NewLine = "\r\n";
            escribir.WriteLine(string.Format("          {0}                                          VALIDACIÓN DE SITUACIÓN LABORAL                                                                               ", string.Format("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)));
            escribir.NewLine = "\r\n";
            escribir.WriteLine(string.Format("                                                              PRESTAMOS QUIROGRAFARIOS"));
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          -------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          R.L       F O L I O       NUM       R. F. C.       N O M B R E       P / D E S C.      S E R I E         I M P.   U N I T.      U L T I M A   A P O R T  ");
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          -------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            escribir.NewLine = "\r\n";
            string permanente = string.Empty;
            int contador = 1;

            int nfolio = 0;
            int nsecuencia = 0;
            int nauxContador = 0;
            int nrfc = 0;
            int nnombre_em = 0;
            int nfecha_descuento = 0;
            int nnum_descuento = 0;
            

            foreach (var item in family)
            {
                string tipoRelacion = Convert.ToString(item["tipo_rel"]);
                if (!string.IsNullOrWhiteSpace(tipoRelacion))
                {
                    if (string.IsNullOrEmpty(permanente))
                        permanente = tipoRelacion;
                    else
                    {
                        if (permanente == tipoRelacion)
                        {
                            goto ir;
                        }
                    }
                    string query = string.Format("select * from catalogos.disket where cuenta = '{0}'", tipoRelacion);
                    List<Dictionary<string, object>> resultado = globales.consulta(query);
                    if (resultado.Count > 0)
                    {
                        string samv = string.Format("                      {0}  =  {1}", tipoRelacion, resultado[0]["descripcion"]);
                        escribir.NewLine = "\r\n";
                        escribir.WriteLine("-");
                        escribir.NewLine = "\r\n";
                        escribir.WriteLine(samv);
                        escribir.NewLine = "\r\n";
                        escribir.WriteLine("-");
                        escribir.NewLine = "\r\n";
                    }
                }

            ir:

                string folio = Convert.ToString(item["folio"]) + "   ";
                string secuencia = Convert.ToString(item["sec"]) + "   ";
                string auxContador = Convert.ToString(contador) + "     ";
                string rfc = Convert.ToString(item["rfc"])+"    ";
                string nombre_em = Convert.ToString(item["nombre_em"]);
                string fecha_descuento = Convert.ToString(item["f_descuento"]).Replace(" 12:00:00 a. m.", "")+"   ";
                string num_descuento = Convert.ToString(item["numdesc"])+"    ";
                string final = Convert.ToString(item["final"])+"   ";
                string aux = "                    {0}.{1}              {2}              {3}              {4}              {5}              {6}              {7}              {8}";

                if (nfolio == 0)
                {
                    nfolio = folio.Length;
                }
                else if (nfolio < folio.Length) {
                    folio = folio.Substring(0, nfolio);
                } else if (nfolio > folio.Length) {
                  folio =  agregarEspacios(folio,nfolio,folio.Length);
                }
                if (nsecuencia == 0)
                {
                    nsecuencia = secuencia.Length;
                } else if (nsecuencia < secuencia.Length) {
                    secuencia = secuencia.Substring(0, nsecuencia);
                } else if (nsecuencia > secuencia.Length) {
                    secuencia = agregarEspacios(secuencia,nsecuencia,secuencia.Length);
                }

                if (nauxContador == 0)
                {
                    nauxContador = auxContador.Length;
                } else if (nauxContador < auxContador.Length) {
                    auxContador = auxContador.Substring(0, nauxContador);
                } else if (nauxContador > auxContador.Length) {
                    auxContador = agregarEspacios(auxContador,nauxContador,auxContador.Length);
                }
                if (nrfc == 0)
                {
                    nrfc = rfc.Length;
                }
                else if (nrfc < rfc.Length) {
                    rfc = rfc.Substring(0, nrfc);
                } else if (nrfc > rfc.Length) {
                    rfc = agregarEspacios(rfc, nrfc,rfc.Length);
                }
                if (nnombre_em == 0)
                {
                    nnombre_em = nombre_em.Length;
                } else if (nnombre_em < nombre_em.Length) {
                    nombre_em = nombre_em.Substring(0, nnombre_em);
                } else if (nnombre_em > nombre_em.Length) {
                    nombre_em = agregarEspacios(nombre_em,nnombre_em,nombre_em.Length);
                }
                if (nfecha_descuento == 0)
                {
                    nfecha_descuento = fecha_descuento.Length;
                } else if (nfecha_descuento < fecha_descuento.Length) {
                    fecha_descuento = fecha_descuento.Substring(0, nfecha_descuento);
                } else if (nfecha_descuento > fecha_descuento.Length) {
                    fecha_descuento = agregarEspacios(fecha_descuento,nfecha_descuento,fecha_descuento.Length);
                }

                if (nnum_descuento == 0)
                {
                    nnum_descuento = num_descuento.Length;
                } else if (nnum_descuento < num_descuento.Length) {
                    num_descuento = num_descuento.Substring(0, nnum_descuento);
                } else if (nnum_descuento > num_descuento.Length) {
                    num_descuento = agregarEspacios(num_descuento,nnum_descuento,num_descuento.Length);
                }

                


                aux = string.Format(aux, folio, secuencia, auxContador, rfc, nombre_em, fecha_descuento, num_descuento, num_descuento, final);
                escribir.WriteLine(aux);
                escribir.NewLine = "\r\n";
                contador++;
            }
            escribir.Close();
            MessageBox.Show("Se termino el reporte..", "Realización de reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StreamReader leer = new StreamReader(rutaPrincipal);
            string cadena = leer.ReadToEnd();
            SISPE_MIGRACION.codigo.herramientas.forms.frmReporteSituacionLaboral situacionLaboral = new codigo.herramientas.forms.frmReporteSituacionLaboral(cadena);
            situacionLaboral.ShowDialog();
        }

        private string agregarEspacios(string txt, int verdadero, int diferencia)
        {
            int cantidad =  verdadero - diferencia;
            for (int x = 0; x < cantidad; x++) {
                txt += " ";
            }
            return txt;
        }
    }
}
