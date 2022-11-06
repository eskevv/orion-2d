using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OrionFramework.UserInterface;

namespace Tester.Main;

public class DialogBox : UiElement
{
    private const float TextDelay = 0.04f;
    private const int Padding = 4;

    private Rectangle _dimensions;
    private IEnumerator<string>? _pushedText;
    private string _displayedText = string.Empty;
    private float _timer;
    private int _textIndex;
    private bool _writing;
    private int _currentLine = 1;
    private bool _paused;

    // -- Arrow Helpers / Init
    
    private bool CanAddChar => _timer >= TextDelay && _textIndex < _pushedText!.Current.Length;
    private Vector2 CharSize => UiManager.UniversalFont.MeasureString("A");
    private Vector2 TextPosition => new(_dimensions.X + Padding, _dimensions.Y + Padding);
    private float MaxLines => _dimensions.Height / CharSize.Y;

    public DialogBox(string identifier, Rectangle dimensions) : base(identifier)
    {
        _dimensions = dimensions;
    }

    // -- Interfaces
    
    public void PushText(IEnumerator<string> pushedText)
    {
        _pushedText = pushedText;
        if (_paused || FlushedText()) return;

        _pushedText.MoveNext();
        _writing = true;
        _textIndex = 0;
    }

    public override void Update()
    {
        if (_paused && Input.Pressed(Keys.X))
            ClearProceed();
        else if (_paused || _pushedText is null) return;

        _timer += Time.DeltaTime;
        CheckLineBreak();
        AddChar();
    }
    
    public override void Draw()
    {
        Batcher.DrawFillRect(_dimensions, Color.GhostWhite);
        Batcher.DrawString(_displayedText, UiManager.UniversalFont, TextPosition, Color.Black);
    }
    
    // -- Internals

    private void AddChar()
    {
        if (!CanAddChar) return;

        _timer = 0f;
        _displayedText += _pushedText!.Current[_textIndex++];
        if (_textIndex == _pushedText.Current.Length)
            EndWriting();
    }

    private void CheckLineBreak()
    {
        float charLength = CharSize.X;
        string removedNewLines = _displayedText.Replace("\n", "");
        int spacesToBlank = _pushedText!.Current.IndexOf(' ', _textIndex) - _textIndex;
        float spaceTakenToBlank = removedNewLines.Length * charLength + spacesToBlank * charLength;

        if (spaceTakenToBlank / _currentLine >= _dimensions.Width - Padding)
        {
            _currentLine++;
            _displayedText += '\n';
        }

        if (_currentLine >= MaxLines)
            _paused = true;
    }

    private void ClearProceed()
    {
        _paused = false;
        _displayedText = string.Empty;
        _currentLine = 1;
    }

    private bool FlushedText()
    {
        if (!_writing) return false;

        while (true)
        {
            CheckLineBreak();
            _displayedText += _pushedText!.Current[_textIndex++];
            if (_textIndex == _pushedText.Current.Length)
            {
                EndWriting();
                return true;
            }
        }
    }

    private void EndWriting()
    {
        _displayedText += ' ';
        _writing = false;
    }
}