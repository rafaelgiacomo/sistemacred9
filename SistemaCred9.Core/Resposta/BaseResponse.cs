using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace SistemaCred9.Core.Resposta
{
    public class BaseResponse
    {
        #region Atributos e Construtores

        private List<string> _friendlyMessages;

        public BaseResponse()
        {
        }

        public BaseResponse(bool success)
        {
            Success = success;
        }

        public BaseResponse(bool success, string[] messages)
        {
            Success = success;
            FriendlyMessages.AddRange(messages);
        }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            FriendlyMessages.Add(message);
        }

        #endregion

        // Propriedades
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public bool HasMessages => FriendlyMessages.Count > 0;

        public string FriendlyMessage => string.Join("|", FriendlyMessages);

        public List<string> FriendlyMessages
        {
            get
            {
                if (_friendlyMessages == null)
                    _friendlyMessages = new List<string>();
                return _friendlyMessages;
            }

            //TODO Remover setter
            set
            {
                _friendlyMessages = value;
            }
        }

        public BaseResponse FailWithMessages(IEnumerable<string> messages, int? errorCode = null)
        {
            Success = false;
            FriendlyMessages.AddRange(messages);
            if (errorCode.HasValue)
                ErrorCode = errorCode.Value;

            return this;
        }

        public BaseResponse FailWithMessage(string message, int? errorCode = null)
        {
            Success = false;
            FriendlyMessages.Add(message);
            if (errorCode.HasValue)
                ErrorCode = errorCode.Value;

            return this;
        }

        public BaseResponse FailWithMessages(ValidationResult validationResult, int? errorCode = null)
        {
            var messages = validationResult.Errors.Select(x => x.ErrorMessage).Distinct().ToArray();
            FailWithMessages(messages, errorCode);

            return this;
        }

        public BaseResponse FailWithMessages(BaseResponse response, int? errorCode = null)
        {
            FailWithMessages(response.FriendlyMessages, errorCode ?? response.ErrorCode);

            return this;
        }
    }
}
