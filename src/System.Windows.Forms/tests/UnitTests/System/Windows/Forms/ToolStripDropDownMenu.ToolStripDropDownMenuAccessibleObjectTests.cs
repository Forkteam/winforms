﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;
using static Interop.UiaCore;

namespace System.Windows.Forms.Tests
{
    public class ToolStripDropDownMenu_ToolStripDropDownMenuAccessibleObjectTests : IClassFixture<ThreadExceptionFixture>
    {
        public static IEnumerable<object[]> ToolStripDropDownMenuAccessible_FragmentNavigate_WithoutItem_TestData()
        {
            IEnumerable<Type> types = ReflectionHelper.GetPublicNotAbstractClasses<ToolStripDropDownItem>().Select(type => type);
            foreach (Type itemType in types)
            {
                foreach (bool createControl in new[] { true, false })
                {
                    yield return new object[] { itemType, createControl };
                }
            }
        }

        [WinFormsTheory]
        [MemberData(nameof(ToolStripDropDownMenuAccessible_FragmentNavigate_WithoutItem_TestData))]
        public void ToolStripDropDownMenuAccessible_FragmentNavigate_ReturnExpected_WithoutItem(Type itemType, bool createControl)
        {
            using ToolStrip toolStrip = new();

            if (createControl)
            {
                toolStrip.CreateControl();
            }

            using ToolStripDropDownItem item = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(itemType);
            toolStrip.Items.Add(item);

            AccessibleObject accessibleObject = item.DropDown.AccessibilityObject;

            Assert.Equal(item.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Null(item.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Null(item.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(createControl, toolStrip.IsHandleCreated);
        }

        [WinFormsTheory]
        [InlineData(true)]
        [InlineData(false)]
        public void ToolStripDropDownMenuAccessible_FragmentNavigate_ReturnExpected_WithoutItem_ToolStripOverflowButton(bool createControl)
        {
            using ToolStrip toolStrip = new();

            if (createControl)
            {
                toolStrip.CreateControl();
            }

            ToolStripOverflowButton item = toolStrip.OverflowButton;

            AccessibleObject accessibleObject = item.DropDown.AccessibilityObject;

            Assert.Equal(item.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Null(item.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Null(item.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(createControl, toolStrip.IsHandleCreated);
        }

        public static IEnumerable<object[]> ToolStripDropDownMenuAccessible_FragmentNavigate_TestData()
        {
            IEnumerable<Type> types = ReflectionHelper.GetPublicNotAbstractClasses<ToolStripDropDownItem>().Select(type => type);
            foreach (Type ownerType in types)
            {
                foreach (Type parentType in types)
                {
                    foreach (Type childType in types)
                    {
                        foreach (bool createControl in new[] { true, false })
                        {
                            yield return new object[] { ownerType, parentType, childType, createControl };
                        }
                    }
                }
            }
        }

        [WinFormsTheory]
        [MemberData(nameof(ToolStripDropDownMenuAccessible_FragmentNavigate_TestData))]
        public void ToolStripDropDownMenuAccessible_FragmentNavigate_ReturnExpected(Type ownerType, Type parentType, Type childType, bool createControl)
        {
            using ToolStrip toolStrip = new();

            if (createControl)
            {
                toolStrip.CreateControl();
            }

            using ToolStripDropDownItem ownerItem = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(ownerType);
            using ToolStripDropDownItem parentItem1 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(parentType);
            using ToolStripDropDownItem parentItem2 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(parentType);
            using ToolStripDropDownItem childItem1 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem2 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem3 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem4 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);

            toolStrip.Items.Add(ownerItem);
            ownerItem.DropDownItems.Add(parentItem1);
            ownerItem.DropDownItems.Add(parentItem2);
            parentItem1.DropDownItems.Add(childItem1);
            parentItem1.DropDownItems.Add(childItem2);
            parentItem2.DropDownItems.Add(childItem3);
            parentItem2.DropDownItems.Add(childItem4);

            AccessibleObject accessibleObject = ownerItem.DropDown.AccessibilityObject;

            Assert.Equal(ownerItem.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(parentItem1.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(parentItem2.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject, ownerItem.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject, ownerItem.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));

            AccessibleObject accessibleObject1 = parentItem1.DropDown.AccessibilityObject;

            Assert.Equal(parentItem1.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject1.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject1.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(childItem1.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(childItem2.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject1, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject1, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject1, childItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject1, childItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));

            AccessibleObject accessibleObject2 = parentItem2.DropDown.AccessibilityObject;

            Assert.Equal(parentItem2.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject2.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject2.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(childItem3.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(childItem4.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject2, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject2, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject2, childItem3.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject2, childItem4.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));

            Assert.Equal(createControl, toolStrip.IsHandleCreated);
        }

        public static IEnumerable<object[]> ToolStripDropDownMenuAccessible_FragmentNavigate_ToolStripOverflowButton_TestData()
        {
            IEnumerable<Type> types = ReflectionHelper.GetPublicNotAbstractClasses<ToolStripDropDownItem>().Select(type => type);
            foreach (Type parentType in types)
            {
                foreach (Type childType in types)
                {
                    foreach (bool createControl in new[] { true, false })
                    {
                        yield return new object[] { parentType, childType };
                    }
                }
            }
        }

        [WinFormsTheory]
        [MemberData(nameof(ToolStripDropDownMenuAccessible_FragmentNavigate_ToolStripOverflowButton_TestData))]
        public void ToolStripDropDownMenuAccessible_FragmentNavigate_ReturnExpected_ToolStripOverflowButton(Type parentType, Type childType)
        {
            using ToolStrip toolStrip = new();
            toolStrip.CreateControl();

            using ToolStripDropDownItem parentItem1 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(parentType);
            using ToolStripDropDownItem parentItem2 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(parentType);
            using ToolStripDropDownItem childItem1 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem2 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem3 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);
            using ToolStripDropDownItem childItem4 = ReflectionHelper.InvokePublicConstructor<ToolStripDropDownItem>(childType);

            toolStrip.Items.Add(parentItem1);
            toolStrip.Items.Add(parentItem2);

            parentItem1.DropDownItems.Add(childItem1);
            parentItem1.DropDownItems.Add(childItem2);
            parentItem2.DropDownItems.Add(childItem3);
            parentItem2.DropDownItems.Add(childItem4);

            using ToolStripOverflowButton ownerItem = toolStrip.OverflowButton;
            toolStrip.OverflowItems.Add(parentItem1);
            toolStrip.OverflowItems.Add(parentItem2);

            AccessibleObject accessibleObject = ownerItem.DropDown.AccessibilityObject;

            Assert.Equal(ownerItem.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(parentItem1.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(parentItem2.AccessibilityObject, accessibleObject.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject, ownerItem.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject, ownerItem.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));

            AccessibleObject accessibleObject1 = parentItem1.DropDown.AccessibilityObject;

            Assert.Equal(parentItem1.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject1.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject1.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(childItem1.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(childItem2.AccessibilityObject, accessibleObject1.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject1, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject1, parentItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject1, childItem1.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject1, childItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));

            AccessibleObject accessibleObject2 = parentItem2.DropDown.AccessibilityObject;

            Assert.Equal(parentItem2.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.Parent));
            Assert.Null(accessibleObject2.FragmentNavigate(NavigateDirection.NextSibling));
            Assert.Null(accessibleObject2.FragmentNavigate(NavigateDirection.PreviousSibling));
            Assert.Equal(childItem3.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(childItem4.AccessibilityObject, accessibleObject2.FragmentNavigate(NavigateDirection.LastChild));

            Assert.Equal(accessibleObject2, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.FirstChild));
            Assert.Equal(accessibleObject2, parentItem2.AccessibilityObject.FragmentNavigate(NavigateDirection.LastChild));
            Assert.Equal(accessibleObject2, childItem3.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
            Assert.Equal(accessibleObject2, childItem4.AccessibilityObject.FragmentNavigate(NavigateDirection.Parent));
        }
    }
}
