﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using ShaderTools.CodeAnalysis.Editor.Shared.Utilities;

namespace ShaderTools.CodeAnalysis.Editor.Implementation.IntelliSense.SignatureHelp.Presentation
{
    [Export(typeof(ISignatureHelpSourceProvider))]
    [Export(typeof(IIntelliSensePresenter<ISignatureHelpPresenterSession, ISignatureHelpSession>))]
    [Name(PredefinedSignatureHelpPresenterNames.RoslynSignatureHelpPresenter)]
    [ContentType(ContentTypeNames.ShaderToolsContentType)]
    internal partial class SignatureHelpPresenter : ForegroundThreadAffinitizedObject, IIntelliSensePresenter<ISignatureHelpPresenterSession, ISignatureHelpSession>, ISignatureHelpSourceProvider
    {
        private static readonly object s_augmentSessionKey = new object();

        private readonly ISignatureHelpBroker _sigHelpBroker;

        [ImportingConstructor]
        public SignatureHelpPresenter(ISignatureHelpBroker sigHelpBroker)
        {
            _sigHelpBroker = sigHelpBroker;
        }

        ISignatureHelpPresenterSession IIntelliSensePresenter<ISignatureHelpPresenterSession, ISignatureHelpSession>.CreateSession(ITextView textView, ITextBuffer subjectBuffer, ISignatureHelpSession sessionOpt)
        {
            AssertIsForeground();
            return new SignatureHelpPresenterSession(_sigHelpBroker, textView, subjectBuffer);
        }

        ISignatureHelpSource ISignatureHelpSourceProvider.TryCreateSignatureHelpSource(ITextBuffer textBuffer)
        {
            AssertIsForeground();
            return new SignatureHelpSource();
        }
    }
}
