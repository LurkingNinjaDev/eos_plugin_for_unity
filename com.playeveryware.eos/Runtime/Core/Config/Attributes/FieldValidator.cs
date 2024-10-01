/*
 * Copyright (c) 2024 PlayEveryWare
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace PlayEveryWare.EpicOnlineServices
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public struct FieldValidatorFailure
    {
        public FieldInfo FieldInfo;
        public FieldValidatorAttribute FailingAttribute;
        public string FailingMessage;

        public FieldValidatorFailure(FieldInfo failingField,
            FieldValidatorAttribute failingAttribute,
            string failingMessage)
        {
            FieldInfo = failingField;
            FailingAttribute = failingAttribute;
            FailingMessage = failingMessage;
        }
    }

    public class FieldValidator
    {
        public static bool TryGetFailingValidatorAttributes(object target, out List<FieldValidatorFailure> failingValidatorAttributes)
        {
            failingValidatorAttributes = new List<FieldValidatorFailure>();

            foreach (FieldInfo curField in target.GetType().GetFields())
            {
                foreach (FieldValidatorAttribute validatorAttribute in curField.GetCustomAttributes(typeof(FieldValidatorAttribute)))
                {
                    if (!validatorAttribute.FieldValueIsValid(curField.GetValue(target), out string problemMessage))
                    {
                        failingValidatorAttributes.Add(new FieldValidatorFailure(curField, validatorAttribute, problemMessage));
                    }
                }
            }

            return failingValidatorAttributes.Count > 0;
        }
    }
}