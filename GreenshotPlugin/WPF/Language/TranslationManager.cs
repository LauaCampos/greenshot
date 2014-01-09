﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2013  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace GreenshotPlugin.WPF {
    public class TranslationManager {
        private static TranslationManager _translationManager;

        public event EventHandler LanguageChanged;

        public static TranslationManager Instance {
            get {
				if (_translationManager == null) {
					_translationManager = new TranslationManager();
				}
                return _translationManager;
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        private void OnLanguageChanged() {
            if (LanguageChanged != null) {
                LanguageChanged(this, EventArgs.Empty);
            }
        }

        public object Translate(string prefix, string key) {
            if( TranslationProvider != null) {
				object translatedValue = TranslationProvider.Translate(prefix, key);
                if (translatedValue != null) {
                    return translatedValue;
                }
            }
			if (prefix == null) {
				return string.Format("!{0}!", key);
			}
			return string.Format("!{0}.{1}!", prefix, key);
        }
    }
}
