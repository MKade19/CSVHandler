using System.Windows;

namespace CSVHandler.UI.Util
{
    public class MessageBoxStore
    {
        /// <summary>
        /// Shows MessageBox for confirmation and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Confirmation(string message)
        {
            return MessageBox.Show
            (
                message,
                (string)Application.Current.FindResource("ConfirmationDialogTitle"),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );;
        }

        /// <summary>
        /// Shows MessageBox for error and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Error(string message)
        {
            return MessageBox.Show
            (
                message,
                (string)Application.Current.FindResource("ErrorDialogTitle"),
                MessageBoxButton.OKCancel,
                MessageBoxImage.Error
            );
        }

        /// <summary>
        /// Shows MessageBox for warning and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Warning(string message)
        {
            return MessageBox.Show
            (
                message,
                (string)Application.Current.FindResource("WarningDialogTitle"),
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning
            );
        }

        /// <summary>
        /// Shows MessageBox for warning and provides its result.
        /// </summary>
        /// <param name="message">Message for MessageBox.</param>
        /// <returns>Result of MessageBox.</returns>
        public static MessageBoxResult Information(string message)
        {
            return MessageBox.Show
            (
                message,
                (string)Application.Current.FindResource("InformationDialogTitle"),
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information
            );
        }
    }
}
