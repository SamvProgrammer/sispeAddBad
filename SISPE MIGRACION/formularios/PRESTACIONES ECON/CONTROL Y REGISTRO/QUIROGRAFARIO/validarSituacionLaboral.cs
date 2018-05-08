using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

                MessageBox.Show("Se va a seleccionar los folios..", "Seleccionando folios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.WaitCursor;
                string fechaDescuento = string.Empty;
                DateTime fecha = fe1.Value;
                fechaDescuento = string.Format("{0}-{1}-{2}", fecha.Year, fecha.Month, fecha.Day);
                string query = string.Format("select *,' ' as final from datos.d_ecqdep where f_descuento = '{0}'", fechaDescuento);
                List<Dictionary<string, object>> resultado = globales.consulta(query);
                foreach (Dictionary<string, object> item in resultado)
                {

                    string rfc = Convert.ToString(item["rfc"]);
                    DateTime f = new DateTime(); ;

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

                List<Dictionary<string, object>> family = resultado;
                MessageBox.Show("Se seleccionara préstamos a descontar el " + fe1.Text, "Seleccionando...", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            StreamWriter escribir = new StreamWriter(@"C:\Users\samv\Documents\Embarcadero\validarSituacionLaboral.txt");
            escribir.NewLine = "\r\n";
            escribir.WriteLine(string.Format("          {0}                                          VALIDACIÓN DE SITUACIÓN LABORAL                                                                               ", string.Format("{0}/{1}/{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)));
            escribir.NewLine = "\r\n";
            escribir.WriteLine(string.Format("                                                              PRESTAMOS QUIROGRAFARIOS"));
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          -------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          R.L       F O L I O          R. F. C.       N O M B R E       P / D E S C.      S E R I E         I M P.   U N I T.      U L T I M A   A P O R T  ");
            escribir.NewLine = "\r\n";
            escribir.WriteLine("          -------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            escribir.NewLine = "\r\n";
            string permanente = string.Empty;
            int contador = 0;
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
                        string samv = string.Format("          {0}  =  {1}", tipoRelacion, resultado[0]["descripcion"]);
                        escribir.WriteLine(samv);
                        escribir.NewLine = "\r\n";
                    }
                }

            ir:
                string aux = "                    {0}               {1}              {2}              {3}              {4}              {5}              {6}              {7}              {8}";
                aux = string.Format(aux, Convert.ToString(item["folio"]), Convert.ToString(item["sec"]), contador, Convert.ToString(item["rfc"]), Convert.ToString(item["nombre_em"]), Convert.ToString(item["f_descuento"]), Convert.ToString(item["numdesc"]), Convert.ToString(item["totdesc"]), Convert.ToString(item["final"]));
                escribir.WriteLine(aux);
                escribir.NewLine = "\r\n";
                contador++;

            }
            escribir.Close();
            MessageBox.Show("Se termino el reporte..", "Realización de reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
