export interface IGoogleTagManger {
    dataLayer: { push: (tag: any) => void };
}
