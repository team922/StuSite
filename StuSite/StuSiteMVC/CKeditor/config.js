/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbarGroups = [
            { name: 'document', groups: ['mode', 'document', 'doctools'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
            { name: 'links', groups: ['links'] },
            { name: 'insert', groups: ['insert'] },
            { name: 'forms', groups: ['forms'] },
            { name: 'others', groups: ['others'] },
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
            { name: 'styles', groups: ['styles'] },
            { name: 'tools', groups: ['tools'] },
            { name: 'colors', groups: ['colors'] },
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
	config.removeButtons = 'Superscript,About,Scayt,Anchor,RemoveFormat,Blockquote,Styles,PasteFromWord,Cut,Strike,Indent,Outdent,Subscript,SpecialChar';

	// Set the most common block elements.
	config.format_tags = 'p;h1;h2;h3;h3;h4;h5;h6';

	// Simplify the dialog windows.
	config.removeDialogTabs = 'image:advanced;link:advanced';
};
