using System;
using Domain.Enums;
using Domain.MessagesDTO;
using MessagePipe;
using Presentation.Views;
using R3;
using VContainer;
using VContainer.Unity;

namespace Presentation.Presenters
{
    public class CellHighlightPresenter : IMessageHandler<CellToHighlightDTO>, IInitializable, IDisposable
    {
        [Inject] private readonly CellHighlightView _cellHighlightView;

        [Inject] private readonly ISubscriber<CellToHighlightDTO> _cellToHighlightDtoSubscriber;

        private CompositeDisposable _disposable = new();

        public void Handle(CellToHighlightDTO message)
        {
            if (!message.IsValid) _cellHighlightView.Hide();

            if (message.HighlightType == HighlightType.Valid)
            {
                _cellHighlightView.ShowSuccess();
            }
            else if (message.HighlightType == HighlightType.Invalid)
            {
                _cellHighlightView.ShowError();
            }

            _cellHighlightView.gameObject.transform.position = message.Position;
        }

        public void Initialize()
        {
            _disposable.Add(
                _cellToHighlightDtoSubscriber.Subscribe(this)
            );
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}