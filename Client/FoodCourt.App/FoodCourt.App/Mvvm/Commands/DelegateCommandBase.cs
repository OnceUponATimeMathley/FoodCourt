using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodCourt.Mvvm.Commands
{
    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates can be attached for <see cref="Execute" /> and <see cref="CanExecute" />
    ///     .
    /// </summary>
    public abstract class DelegateCommandBase : ICommand
    {
        protected readonly Func<object, Task> ExecuteMethod;

        private readonly HashSet<string> _propertiesToObserve = new HashSet<string>();

        private readonly SynchronizationContext _synchronizationContext;
        protected Func<object, bool> CanExecuteMethod;
        private INotifyPropertyChanged _inpc;
        private bool _isActive;

        /// <summary>
        ///     Creates a new instance of a <see cref="DelegateCommandBase" />, specifying both the execute action and the can
        ///     execute function.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to execute when <see cref="ICommand.Execute" /> is invoked.</param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{Object,Bool}" /> to invoked when <see cref="ICommand.CanExecute" />
        ///     is invoked.
        /// </param>
        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));

            ExecuteMethod = arg =>
            {
                executeMethod(arg);
                return Task.Delay(0);
            };
            CanExecuteMethod = canExecuteMethod;
            _synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        ///     Creates a new instance of a <see cref="DelegateCommandBase" />, specifying both the Execute action as an awaitable
        ///     Task and the CanExecute function.
        /// </summary>
        /// <param name="executeMethod">
        ///     The <see cref="Func{Object,Task}" /> to execute when <see cref="ICommand.Execute" /> is
        ///     invoked.
        /// </param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{Object,Bool}" /> to invoked when <see cref="ICommand.CanExecute" />
        ///     is invoked.
        /// </param>
        protected DelegateCommandBase(Func<object, Task> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod));

            ExecuteMethod = executeMethod;
            CanExecuteMethod = canExecuteMethod;
            _synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        ///     Raises <see cref="ICommand.CanExecuteChanged" /> so every
        ///     command invoker can requery <see cref="ICommand.CanExecute" />.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                    _synchronizationContext.Post(o => handler.Invoke(this, EventArgs.Empty), null);
                else
                    handler.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Executes the command with the provided parameter by invoking the <see cref="Action{Object}" /> supplied during
        ///     construction.
        /// </summary>
        /// <param name="parameter"></param>
        protected virtual async Task Execute(object parameter)
        {
            await ExecuteMethod(parameter);
        }

        /// <summary>
        ///     Determines if the command can execute with the provided parameter by invoking the <see cref="Func{Object,Bool}" />
        ///     supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns <see langword="true" /> if the command can execute.  <see langword="False" /> otherwise.</returns>
        protected virtual bool CanExecute(object parameter)
        {
            return CanExecuteMethod(parameter);
        }

        protected void HookInpc(MemberExpression expression)
        {
            if (expression == null)
                return;

            if (_inpc == null)
                if (expression.Expression is ConstantExpression constantExpression)
                {
                    _inpc = constantExpression.Value as INotifyPropertyChanged;
                    if (_inpc != null)
                        _inpc.PropertyChanged += Inpc_PropertyChanged;
                }
        }

        protected void AddPropertyToObserve(string property)
        {
            if (_propertiesToObserve.Contains(property))
                throw new ArgumentException(string.Format("{0} is already being observed.", property));

            _propertiesToObserve.Add(property);
        }

        /// <summary>
        ///     Raises <see cref="DelegateCommandBase.CanExecuteChanged" /> so every command invoker
        ///     can requery to check if the command can execute.
        ///     <remarks>
        ///         Note that this will trigger the execution of <see cref="DelegateCommandBase.CanExecute" /> once for each
        ///         invoker.
        ///     </remarks>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        private void Inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_propertiesToObserve.Contains(e.PropertyName))
                RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public virtual event EventHandler CanExecuteChanged;

        async void ICommand.Execute(object parameter)
        {
            await Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            AddPropertyToObserve(PropertySupport.ExtractPropertyName(propertyExpression));
            HookInpc(propertyExpression.Body as MemberExpression);
        }

        /// <summary>
        ///     Observes a property that is used to determine if this command can execute, and if it implements
        ///     INotifyPropertyChanged it will automatically call DelegateCommandBase.RaiseCanExecuteChanged on property changed
        ///     notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute((o) => PropertyName).</param>
        /// <returns>The current instance of DelegateCommand</returns>
        protected internal void ObservesCanExecuteInternal(Expression<Func<object, bool>> canExecuteExpression)
        {
            CanExecuteMethod = canExecuteExpression.Compile();
            AddPropertyToObserve(PropertySupport.ExtractPropertyNameFromLambda(canExecuteExpression));
            HookInpc(canExecuteExpression.Body as MemberExpression);
        }

        #region IsActive

        /// <summary>
        ///     Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnIsActiveChanged();
                }
            }
        }

        /// <summary>
        ///     Fired if the <see cref="IsActive" /> property changes.
        /// </summary>
        public virtual event EventHandler IsActiveChanged;

        /// <summary>
        ///     This raises the <see cref="DelegateCommandBase.IsActiveChanged" /> event.
        /// </summary>
        protected virtual void OnIsActiveChanged()
        {
            var isActiveChangedHandler = IsActiveChanged;
            if (isActiveChangedHandler != null) isActiveChangedHandler(this, EventArgs.Empty);
        }

        #endregion
    }
}