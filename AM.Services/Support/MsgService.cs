using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Services.Helpers
{
	public class MsgService
	{
		public class Operation
		{
			public static string SuccessfullySaved(string itemName)
			{
				//return $"{itemName} has been succesfully saved.";
				return $"{itemName} ha sido guardado correctamente.";
			}

			public static string SuccessfullyUpdated(string itemName)
			{
				//return $"{itemName} has been succesfully updated.";
				return $"{itemName} ha sido actualizado correctamente.";
			}

			public static string SuccessfullyAdded(string itemName)
			{
				//return $"{itemName} has been succesfully added.";
				return $"{itemName} ha sido agregado correctamente.";
			}

			public static string SuccessfullyRemoved(string itemName)
			{
				//return $"{itemName} has been succesfully removed.";
				return $"{itemName} ha sido borrado correctamente.";
			}

            public static string SuccessfullyReactivated(string itemName)
            {
                return $"{itemName} has been succesfully reactivated.";
            }

			public static string SuccessfullyConsolidated(string itemName)
			{
				return $"{itemName} has been succesfully consolidated.";
			}

			public static string ErrorSaving(string itemName)
			{
				//return $"An error ocurred saving the {itemName}.";
				return $"Un error ha ocurrido guardando {itemName}.";
			}

			public static string ConcurrencyException(string itemName)
			{
				//return $"This {itemName} has been modified before you commited your changes. Please start again.";
				return $"Este {itemName} ha sido modificado antes de guardar sus cambios. Por favor comienze nuevamente.";
			}
		}

		public static class Validation
		{
			public static string RequiredField(string fieldName)
			{
				//return $"{fieldName} is a required field.";
				return $"{fieldName} es un campo requerido.";
			}

			public static string InvalidInput(string fieldName)
			{
				//return $"The entered {fieldName} is invalid.";
				return $"El {fieldName} ingresado es inválido.";
			}

			public static string UniqueName(string fieldName)
			{
				//return $"{fieldName} name already exist. Please provide a unique name.";
				return $"El nombre de {fieldName} ya existe. Por favor provea un nombre único.";
			}

			public static string CanDeleteItemBeingUsed(string fieldName)
			{
				//return $"This {fieldName} can not be deleted because it is being used.";
				return $"Este {fieldName} no puede ser borrado porque esta siendo utilizado.";
			}

			public static string SelectAtLeastOne(string fieldName)
            {
				//return $"Please select at least one {fieldName}.";
				//return $" {fieldName}.";
				return $" Por favor seleccione al menos un {fieldName}.";
			}

			public static string DoesNotExist(string fieldName)
			{
				//return $"The entered {fieldName} does not exist.";
				return $"El {fieldName} ingresado no existe.";
			}
		}
	}
}
