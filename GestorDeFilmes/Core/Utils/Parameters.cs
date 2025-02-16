using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeFilmes.Core.Utils
{
    /// <summary>
    /// Classe do tipo Singleton para armazenar e gerenciar parâmetros temporários e utilizar para passar dados/objetos entre as ViewModels.
    /// Podendo também ser usada em qualquer parte do projeto.
    /// </summary>
    public class Parameter
    {
        private static Parameter instance;
        private readonly Dictionary<string, object> parameters;

        public Parameter()
        {
            parameters = new Dictionary<string, object>();
        }
        /// <summary>
        /// Propriedade que retorna a instância única.
        /// Se não existir uma instância, ela é criada.
        /// </summary>
        public static Parameter Instance
        {
            get
            {
                instance ??= new Parameter();
                return instance;
            }
        }

        /// <summary>
        /// Adiciona ou atualiza um parâmetro.
        /// </summary>
        public void AddParameter(string key, object value)
        {
            parameters[key] = value;
        }

        /// <summary>
        /// Obtém e remove um parâmetro do dicionário.
        /// Se a chave existir, retorna o valor e remove do dicionário.
        /// Caso contrário, retorna null.
        /// </summary>
        public object GetParameter(string key)
        {
            if (parameters.TryGetValue(key, out var value))
            {
                parameters.Remove(key);
                return value;
            }

            return null;
        }

        /// <summary>
        /// Remove um parâmetro do dicionário se ele existir, passando a chave.
        /// </summary>
        public void RemoveParameter(string key)
        {
            if (parameters.TryGetValue(key, out var value))
                parameters.Remove(key);
        }

        /// <summary>
        /// Tenta obter um parâmetro e removê-lo do dicionário.
        /// Retorna true se o parâmetro existir e for recuperado com sucesso.
        /// </summary>
        public bool TryGetParameter(string key, out object value)
        {
            value = GetParameter(key);
            return value != null;
        }
    }
}
