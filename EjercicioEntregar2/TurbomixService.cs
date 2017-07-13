﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioEntregar2
{
    public class TurbomixService
    {
        public IBascula Bascula { get; set; }
        public ICocina Cocina { get; set; }
        public IRecetaRepository Recetario { get; set; }

        public TurbomixService(IBascula _Bascula, ICocina _Cocina)
        {
            this.Bascula = _Bascula;
            this.Cocina = _Cocina;
        }

        public Plato PrepararPlato(Alimento mAlimento1, Alimento mAlimento2)
        {
            float Peso1 = Bascula.Pesar(mAlimento1);
            float Peso2 = Bascula.Pesar(mAlimento2);
            Cocina.Calentar(mAlimento1, mAlimento2);

            return new Plato(mAlimento1, mAlimento2);
        }

        public Plato PrepararPlato(Alimento mAlimento1, Alimento mAlimento2, Receta Receta)
        {
            Plato PlatoFinal = null;
            float Peso1 = Bascula.Pesar(mAlimento1);
            float Peso2 = Bascula.Pesar(mAlimento2);
            if (MismoNombreAlimentosConReceta(mAlimento1, mAlimento2, Receta))
            {
                if (!mAlimento1.Calentado && !mAlimento2.Calentado)
                {
                    if (Peso1 >= Bascula.Pesar(Receta.Alimento1) && Peso2 >= Bascula.Pesar(Receta.Alimento2))
                    {
                        if (Peso1 > Bascula.Pesar(Receta.Alimento1))
                        {
                            mAlimento1.Peso = Receta.Alimento1.Peso;
                        }
                        if (Peso2 > Bascula.Pesar(Receta.Alimento2))
                        {
                            mAlimento2.Peso = Receta.Alimento2.Peso;
                        }

                        Cocina.Calentar(mAlimento1, mAlimento2);

                        PlatoFinal = new Plato(mAlimento1, mAlimento2);
                    }
                }
            }
            return PlatoFinal;

        }

        public void GuardarReceta(Alimento mAlimento1, Alimento mAlimento2)
        {
            Receta Receta1 = new Receta(mAlimento1,mAlimento2);
            Recetario.CreateReceta(Receta1);
        }

        private static bool MismoNombreAlimentosConReceta(Alimento mAlimento1, Alimento mAlimento2, Receta Receta)
        {
            return mAlimento1.Nombre.Equals(Receta.Alimento1.Nombre)
                            && mAlimento2.Nombre.Equals(Receta.Alimento2.Nombre);
        }
    }
}
