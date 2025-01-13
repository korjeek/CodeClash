import React from 'react';
import {Remarkable} from 'remarkable';
import katex from "katex";

interface MarkdownRendererProps {
    markdown: string;
}

const MarkdownRenderer: React.FC<MarkdownRendererProps> = ({ markdown }) => {
    const md = new Remarkable('commonmark');

    // Переопределение метода renderer для обработки математических выражений
    md.renderer.rules.text = (tokens, idx) => {
        const token = tokens[idx];
        if (token.type === 'text') {
            const mathPattern = /\$([^$]+)\$/g;
            const text = token.content;

            return text.replace(mathPattern, (_match, p1) => renderMath(p1))
        }
        return md.renderer.renderInline([token], md.renderer.options, {});
    };

    const createMarkup = () => {
        return { __html: md.render(markdown) };
    };

    return <div dangerouslySetInnerHTML={createMarkup()} />;
};

const renderMath = (text: string) => {
    try {
        return katex.renderToString(text, {
            throwOnError: false,
            output: 'mathml',
            displayMode: false
        });
    } catch (error) {
        console.error('KaTeX rendering error:', error);
        return text;
    }
};

export default MarkdownRenderer;