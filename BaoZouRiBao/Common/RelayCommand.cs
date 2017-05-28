﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BaoZouRiBao.Common
{
    /// <summary>
    /// 专门用于中继自身功能的命令
    /// 通过调用委托分配给其他对象。
    ///CanExecute 方法的默认返回值为“true”。
    /// 在下列情况中，始终需要调用 <see cref="RaiseCanExecuteChanged"/>
    /// <see cref="CanExecute"/> 应返回其他的值。
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithPara;
        private readonly Func<bool> _canExecute;
        private readonly System.Action _execute;
        /// <summary>
        /// 调用 RaiseCanExecuteChanged 时引发。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 创建可始终执行的新命令。
        /// </summary>
        /// <param name="execute">执行逻辑。</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 创建可始终执行的新命令。
        /// </summary>
        /// <param name="execute">执行逻辑。</param>
        public RelayCommand(System.Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 创建新命令。
        /// </summary>
        /// <param name="execute">执行逻辑。</param>
        /// <param name="canExecute">执行状态逻辑。</param>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _executeWithPara = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 创建新命令。
        /// </summary>
        /// <param name="execute">执行逻辑。</param>
        /// <param name="canExecute">执行状态逻辑。</param>
        public RelayCommand(System.Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

   
        

        /// <summary>
        /// 确定此 <see cref="RelayCommand"/> 是否可在其当前状态下执行。
        /// </summary>
        /// <param name="parameter">
        /// 命令使用的数据。如果不需要向命令传递数据，则可将此对象设置为 null。
        /// </param>
        /// <returns>如果可执行此命令，则返回 true；否则返回 false。</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        /// <summary>
        /// 对当前命令目标执行 <see cref="RelayCommand"/>。
        /// </summary>
        /// <param name="parameter">
        /// 命令使用的数据。如果不需要向命令传递数据，则可将此对象设置为 null。
        /// </param>
        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else
            {
                _executeWithPara(parameter);
            }
        }




        /// <summary>
        /// 用于引发 <see cref="CanExecuteChanged"/> 事件的方法
        /// 执行 <see cref="CanExecute"/> 的返回值
        /// 方法已更改。
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
