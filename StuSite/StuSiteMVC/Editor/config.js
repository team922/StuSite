/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	config.toolbarGroups = [
		{ name: 'document', groups: [ 'mode', 'document', 'doctools' ] },
		{ name: 'clipboard', groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing', groups: [ 'find', 'selection', 'spellchecker', 'editing' ] },
		{ name: 'forms', groups: [ 'forms' ] },
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph', groups: [ 'list', 'indent', 'blocks', 'align', 'bidi', 'paragraph' ] },
		{ name: 'links', groups: [ 'links' ] },
		{ name: 'insert', groups: [ 'insert' ] },
		{ name: 'styles', groups: [ 'styles' ] },
		{ name: 'colors', groups: [ 'colors' ] },
		{ name: 'tools', groups: [ 'tools' ] },
		{ name: 'others', groups: [ 'others' ] },
		{ name: 'about', groups: [ 'about' ] }
	];

	config.removeButtons = 'Save,NewPage,Preview,Print,Templates,PasteFromWord,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,RemoveFormat,Blockquote,CreateDiv,Language,BidiLtr,BidiRtl,Link,Unlink,Anchor,Flash,PageBreak,Iframe,Form,Maximize,ShowBlocks,About';

	config.language = 'zh-cn';
	config.width = 700;
	config.height = 300;
	config.toolbarCanCollapse = false;
	config.resize_enabled = false;

    ////改变大小的最大高度
	//config.resize_maxHeight = 3000;

    ////改变大小的最大宽度
	//config.resize_maxWidth = 3000;

    ////改变大小的最小高度
	//config.resize_minHeight = 250;

    ////改变大小的最小宽度
	//config.resize_minWidth = 750
};