using System;

namespace CommandSystem
{
    public class DelegatingCommand : ICommand
    {

        private Func<bool> _commit;
        private Func<bool> _rollback;

        public DelegatingCommand(Func<bool> commit, Func<bool> rollback)
        {
            _commit = commit;
            _rollback = rollback;
        }

        public bool Rollback()
            => _rollback();

        public bool Commit()
            => _commit();
    }

}