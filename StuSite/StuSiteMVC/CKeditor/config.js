/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];
    config.filebrowserUploadUrl = "/Admin/ImageUpload";

    config.language = 'zh-cn';
    config.width = 700;
    config.height = 300;
    config.toolbarCanCollapse = false;
    config.resize_enabled = false;

    // Remove some buttons provided by the standard plugins, which are
    // not needed in the Standard(s) toolbar.
    config.removeButtons = 'NewPage,Templates,Cut,Copy,PasteFromWord,Find,Replace,SelectAll,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Strike,RemoveFormat,NumberedList,Outdent,Indent,BulletedList,Blockquote,BidiLtr,BidiRtl,Anchor,Flash,Smiley,SpecialChar,PageBreak,Iframe,Styles,ShowBlocks,Link,Unlink,Font,About,Language';

    // Set the most common block elements.
    config.format_tags = 'p;h1;h2;h3;h3;h4;h5;h6';

    // Simplify the dialog windows.
    config.removeDialogTabs = 'image:advanced;link:advanced';
};
