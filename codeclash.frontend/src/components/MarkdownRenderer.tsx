import React from 'react';
import { Remarkable } from 'remarkable';

interface MarkdownRendererProps {
    markdown: string;
}

const MarkdownRenderer: React.FC<MarkdownRendererProps> = ({ markdown }) => {
    const md = new Remarkable('commonmark');

    const createMarkup = () => {
        return { __html: md.render(markdown) };
    };

    return <div dangerouslySetInnerHTML={createMarkup()} />;
};

export default MarkdownRenderer;