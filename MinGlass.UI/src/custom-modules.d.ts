// Tell ts compiler that we allow importing *.module.css files as module
declare module '*.module.less' {
    const classes: { readonly [key: string]: string };
    export default classes;
}
declare module '*.png';
declare module '*.jpg';
declare module '*.jpeg';
