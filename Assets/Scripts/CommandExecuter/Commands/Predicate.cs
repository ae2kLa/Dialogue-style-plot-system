using plot_command_executor;

namespace plot_command_executor_fgui
{
    public class Predicate : ICommand
    {
        int value = -1;

        public void Execute()
        {
            //不断出队，直至队头为 Predicate value == -1 的下一个命令
            while (CommandSender.Instance.PeekCommand() != null)
            {
                ICommand command = CommandSender.Instance.DequeueCommand();
                Predicate p = command as Predicate;
                
                if(p != null && p.value == -1)
                {
                    break;
                }
            }
        }

        public void OnUpdate()
        {

        }

        public bool IsFinished()
        {
            return true;
        }

    }

}
