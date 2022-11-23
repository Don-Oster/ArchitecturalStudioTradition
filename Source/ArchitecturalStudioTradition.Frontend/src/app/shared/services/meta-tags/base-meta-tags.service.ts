import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { Meta, MetaDefinition } from '@angular/platform-browser';

@Injectable()
export class BaseMetaTagsService {

  constructor(
    private readonly metaService: Meta,
    @Inject(DOCUMENT) private readonly dom: Document
  ) { }

  setMetaTag(property: string, content: string) {
    const def: MetaDefinition = {
      property,
      content
    };

    if (this.metaService.getTag(`property='${def.property}'`)) {
      this.metaService.updateTag(def);
    } else {
      this.metaService.addTag(def);
    }
  }

  setCanonicalLink(value: string) {
    let linkElement: HTMLLinkElement = this.dom.querySelector('link[rel="canonical"]');

    if (!linkElement) {
      linkElement = this.dom.createElement('link');
      linkElement.setAttribute('rel', 'canonical');
      this.dom.head.appendChild(linkElement);
    }

    linkElement.setAttribute('href', value);
  }
}
